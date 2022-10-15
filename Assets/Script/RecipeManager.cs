using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;

    public List<LevelRecipe> RecipeBook = new List<LevelRecipe>();

    public FoodTypesReferenceHolder FoodTypesReferenceHolder;

    public void Awake()
    {
        Instance = this;
    }


}