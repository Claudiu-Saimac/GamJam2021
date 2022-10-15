using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodSocket : MonoBehaviour
{
    public LevelRecipe LevelRecipe;

    public List<FoodTypes> CurrentFood = new List<FoodTypes>();

    public Image SocketImage;

    public FoodStep _lastFoodStep;

    public Button BackButton;
   
    public void Awake()
    {
        BackButton.onClick.AddListener(Back);
    }

    private void OnDestroy()
    {
        BackButton.onClick.RemoveListener(Back);
    }
    private void Back()
    {
        Undo();
    }

    public bool CheckFoodLogic(FoodItem foodItem)
    {
        var list = new List<FoodTypes>();
        list.AddRange(CurrentFood);

        list.Add(foodItem.FoodType);

        var result = LevelRecipe.ReturnFoodStep(list);
        if (result == null)
            return false;

        _lastFoodStep = result;

        result.FoodEvent.Invoke();

        if (result.Sprite != null)
        {
            SocketImage.color = new Color(255, 255, 255, 255);
            SocketImage.sprite = result.Sprite;
        }

        CurrentFood.Add(foodItem.FoodType);

        return true;
    }

    public void Undo()
    {
        switch (CurrentFood.Count)
        {
            case 0:
                return;
            case 1:
                LevelManager.Instance.Undo(CurrentFood[0]);
                
                SocketImage.color = new Color(255, 255, 255, 0);
                CurrentFood.Remove(CurrentFood[0]);

                LevelManager.Instance.Text.text = string.Empty;

                return;
        }

        var lastFoodItem = CurrentFood[^1];
        LevelManager.Instance.Undo(lastFoodItem);
        LevelManager.Instance.Text.text = string.Empty;

        CurrentFood.Remove(lastFoodItem);

        var list = new List<FoodTypes>();
        list.AddRange(CurrentFood);

        var result = LevelRecipe.ReturnFoodStep(list);
        if (result == null)
            return;

        _lastFoodStep = result;

        if (result.Sprite != null)
        {
            SocketImage.color = new Color(255, 255, 255, 255);
            SocketImage.sprite = result.Sprite;
        }

    }
}
