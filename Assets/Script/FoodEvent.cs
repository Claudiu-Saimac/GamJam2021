using System;

[Serializable]
public class FoodEvent
{
    public string Dialog;

    public bool LastStep;

    public virtual void Invoke()
    {
        LevelManager.Instance.Text.text = Dialog;
    }
}