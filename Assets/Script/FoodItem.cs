using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Space]
    public FoodTypes FoodType;

    [Space]
    public LayerMask LayerMask;
    public Vector2 ColliderSize = new Vector2(2, 2);
    public Vector2 ColliderOffset;

    private Vector3 _startPosition;

    public ItemHolder ItemHolder { get; set; }

    private Vector3 offset;

    public void Awake()
    {
        _startPosition = gameObject.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(LevelManager.Instance.ItemDragHolder.transform);
        
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePos))
        {
            offset = transform.position - globalMousePos;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePos))
        {
            transform.position = globalMousePos + offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(LevelManager.Instance.Content.transform);

        var socket = CheckSocket();
        if (socket == null)
        {
            ItemHolder.Reset();
            Destroy(gameObject);
            return;
        }
        
        var result =socket.CheckFoodLogic(this);
        if(result==false)
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
