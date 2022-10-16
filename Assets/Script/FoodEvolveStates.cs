using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu(menuName = "FoodEvolveStates", fileName = "FoodEvolveStates")]
public class FoodEvolveStates : ScriptableObject
{
    public List<FoodEvolveRef> FoodsEvolve = new List<FoodEvolveRef>();

    public FoodTypes GetBaseState(FoodTypes state)
    {
        foreach (var food in FoodsEvolve)
        {
            if (food.Evolve == state)
                return food.Base;
        }

        return state;
    }
}
[Serializable]
public class FoodEvolveRef
{
    public FoodTypes Base;
    public FoodTypes Evolve;
}
