using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu(menuName = "LevelRecipe", fileName = "Recipe")]
public class LevelRecipe : ScriptableObject
{
    public List<Recipe> Recipes = new List<Recipe>();

    public List<FoodTypes> LevelAllFoodTypes = new List<FoodTypes>();

    public FoodStep ReturnFoodStep(List<FoodTypes> order)
    {
        foreach (var recipe in Recipes)
        {
            bool ok = true;
            for (int i = 0; i < order.Count; i++)
            {
                if (recipe.Steps.Count - 1 < i)
                {
                    ok = false;
                    break;
                }
                if (recipe.Steps[i].Type != order[i])
                {
                    ok = false;
                    break;
                }
            }

            if (ok)
                return recipe.Steps[order.Count - 1];
        }

        return null;
    }

}