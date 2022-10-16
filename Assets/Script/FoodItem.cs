using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FoodItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Space]
    public FoodTypes FoodType;

    public bool CanEvolve = false;
    public FoodTypes EvolveTypes;
    public Sprite EvolvedImage;

    [Space]
    public LayerMask LayerMask;
    public Vector2 ColliderSize = new Vector2(2, 2);
    public Vector2 ColliderOffset;

    private Vector3 _startPosition;

    public ItemHolder ItemHolder { get; set; }

    private Vector3 offset;
    private Image _image;

    public void Awake()
    {
        _startPosition = gameObject.transform.position;
        _image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!LevelManager.Instance.GameRunning)
            return;

        transform.SetParent(LevelManager.Instance.ItemDragHolder.transform);

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePos))
        {
            offset = transform.position - globalMousePos;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!LevelManager.Instance.GameRunning)
            return;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePos))
        {
            transform.position = globalMousePos + offset;
        }

        var panSocket = CheckPanSocket();

        if (panSocket != null)
        {
            if (panSocket.CheckFoodEvolve(this))
                _image.sprite = EvolvedImage;
        }

        if(FoodType!=FoodTypes.Egg)
            return;

        var cutting = CheckCuttingBoardSocket();

        if (cutting != null)
        {
            cutting.CheckEvolveEgg(this, _image);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!LevelManager.Instance.GameRunning)
            return;

        transform.SetParent(LevelManager.Instance.Content.transform);

        var socket = CheckSocket();

        var cutting = CheckCuttingBoardSocket();

        var panSocket = CheckPanSocket();

        if (socket == null && panSocket == null && cutting == null)
        {
            ItemHolder.Reset();
            Destroy(gameObject);
            return;
        }

        if (socket != null)
        {
            var result = socket.CheckFoodLogic(this);

            if (result == false)
            {
                ItemHolder.Reset();
                Destroy(gameObject);
            }
            else
            {
                ItemHolder.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        if (panSocket != null)
        {
            var result = panSocket.CheckFoodPan(this);

            if (result == false)
            {
                ItemHolder.Reset();
                Destroy(gameObject);
            }
            else
            {
                ItemHolder.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        if (cutting != null)
        {
            var result = cutting.CheckForBowl(this);

            if (result == false)
            {
                ItemHolder.Reset();
                Destroy(gameObject);
            }
            else
            {
                ItemHolder.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }


    }

    private PanSocket CheckPanSocket()
    {
        var colliderPool = new List<Collider2D>(10);

        var contactFilter = new ContactFilter2D()
        {
            layerMask = LayerMask,
            useTriggers = true,
            useLayerMask = true,
        };

        var count = Physics2D.OverlapBox((Vector2)transform.position + ColliderOffset, ColliderSize, 0,
            contactFilter, colliderPool);

        var scripType = typeof(PanSocket);

        for (var i = 0; i < count; i++)
        {
            var comp = colliderPool[i].GetComponent(scripType);

            if (comp == null)
                continue;

            var socket = (PanSocket)comp;

            return socket;
        }

        return null;
    }

    private CuttingBoardSocket CheckCuttingBoardSocket()
    {
        var colliderPool = new List<Collider2D>(10);

        var contactFilter = new ContactFilter2D()
        {
            layerMask = LayerMask,
            useTriggers = true,
            useLayerMask = true,
        };

        var count = Physics2D.OverlapBox((Vector2)transform.position + ColliderOffset, ColliderSize, 0,
            contactFilter, colliderPool);

        var scripType = typeof(CuttingBoardSocket);

        for (var i = 0; i < count; i++)
        {
            var comp = colliderPool[i].GetComponent(scripType);

            if (comp == null)
                continue;

            var socket = (CuttingBoardSocket)comp;

            return socket;
        }

        return null;
    }

    private FoodSocket CheckSocket()
    {
        var colliderPool = new List<Collider2D>(10);

        var contactFilter = new ContactFilter2D()
        {
            layerMask = LayerMask,
            useTriggers = true,
            useLayerMask = true,
        };

        var count = Physics2D.OverlapBox((Vector2)transform.position + ColliderOffset, ColliderSize, 0,
            contactFilter, colliderPool);

        var scripType = typeof(FoodSocket);

        for (var i = 0; i < count; i++)
        {
            var comp = colliderPool[i].GetComponent(scripType);

            if (comp == null)
                continue;

            var socket = (FoodSocket)comp;

            return socket;
        }

        return null;
    }

}
