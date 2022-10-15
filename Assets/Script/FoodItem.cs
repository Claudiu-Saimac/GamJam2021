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

    private List<Collider2D> _colliderPool = new List<Collider2D>(10);

    private Vector3 _startPosition;

    public ItemHolder ItemHolder { get; set; }

    public void Awake()
    {
        _startPosition = gameObject.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out var globalMousePos))
        {
            transform.position = globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var socket = CheckSocket();
        if (socket == null)
        {
            ItemHolder.Reset();
            Destroy(gameObject);
            return;
        }
        Debug.LogWarning("salut");

        socket.CheckFoodLogic(this);
    }

    private FoodSocket CheckSocket()
    {
        var contactFilter = new ContactFilter2D()
        {
            layerMask = LayerMask,
            useTriggers = true,
            useLayerMask = true,
        };

        var count = Physics2D.OverlapBox((Vector2)transform.position + ColliderOffset, ColliderSize, 0,
            contactFilter, _colliderPool);

        var scripType = typeof(FoodSocket);

        for (var i = 0; i < count; i++)
        {
            var comp = _colliderPool[i].GetComponent(scripType);

            Debug.LogWarning(comp);

            if (comp == null)
                continue;

            Debug.LogWarning(comp);

            var socket = (FoodSocket)comp;

            return socket;
        }

        return null;
    }
}
