using UnityEngine;
using UnityEngine.EventSystems;

public class ItemHolder : MonoBehaviour, IPointerClickHandler
{
    public FoodItem FoodItemPrefab;

    [Space]
    public Sprite IconText;
    public FoodTypes FoodType;

    private FoodItem _itemInstance;

    public void Awake()
    {
        Reset();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
    }

    public void Reset()
    {
        _itemInstance = Instantiate(FoodItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        _itemInstance.gameObject.transform.position = transform.position;
        _itemInstance.gameObject.transform.SetParent(transform);
        _itemInstance.gameObject.transform.localScale = Vector3.one;

        _itemInstance.ItemHolder = this;
    }
}

public enum FoodTypes
{
    Milk
}

