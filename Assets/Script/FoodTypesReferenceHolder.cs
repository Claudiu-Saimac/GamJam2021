using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu(menuName = "FoodTypesReferenceHolder", fileName = "FoodTypesReferenceHolder")]
public class FoodTypesReferenceHolder :ScriptableObject
{
    public  List<FoodTypeReference> FoodRefs = new List<FoodTypeReference>();

    public  FoodItem GetFoodItemByType(FoodTypes type)
    {
        foreach (var food in FoodRefs)
        {
            if (food.Type == type)
                return food.FoodItem;
        }

        return null;
    }
    
}