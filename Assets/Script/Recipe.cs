using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Recipe
{
    public List<FoodStep> Steps = new List<FoodStep>();
}

[Serializable]
public class FoodStep
{
    public FoodTypes Type;
    public Sprite Sprite;

    public FoodEvent FoodEvent;

}