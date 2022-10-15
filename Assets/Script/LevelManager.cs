using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public LevelRecipe LevelRecipe;
    public FoodSocket FoodSocket;

    public GameObject Content;
    public ItemHolder ItemHolderPrefab;

    private List<ItemHolder> _itemHolders = new List<ItemHolder>();

    public GameObject ItemDragHolder;
    public Clock Clock;

    public bool GameRunning { get; set; }

    public void Awake()
    {
        Instance = this;
        SetRecipe();
        StartTimer();
        SetFoodItems();
        GameRunning = true;
    }

    private void OnDestroy()
    {
        Clock.OnTimeEnded -= Clock_OnTimeEnded;
    }
    private void StartTimer()
    {
        Clock.StartClock(60);
        Clock.OnTimeEnded += Clock_OnTimeEnded;
    }

    private void Clock_OnTimeEnded()
    {
        PlayerFailed();
    }

    private void SetFoodItems()
    {
        foreach (var foodType in LevelRecipe.LevelAllFoodTypes)
        {
            var go = Instantiate(ItemHolderPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            go.gameObject.transform.position = new Vector3(0,0,0);
            go.gameObject.transform.SetParent(Content.transform);
            go.gameObject.transform.localScale = Vector3.one;

            go.FoodType = foodType;
            go.FoodItemPrefab = RecipeManager.Instance.FoodTypesReferenceHolder.GetFoodItemByType(foodType);
            go.Reset();

            _itemHolders.Add(go);
        }
       
    }

    public void PlayerFailed()
    {
        GameRunning = false;
        DialogManager.Instance.SetText("You Failed!");
        
    }
    public void PlayerFinished()
    {
        Clock.Stop();
        GameRunning = false;
    }

    public void Undo(FoodTypes type)
    {
        foreach (var holder in _itemHolders)
        {
            if (holder.FoodType == type)
            {
                holder.gameObject.SetActive(true);
                holder.Reset();
                return;
            }
        }
    }

    private void SetRecipe()
    {
        FoodSocket.LevelRecipe = LevelRecipe;
    }
}
