using System;

[Serializable]
public class FoodEvent
{
    public string Dialog;

    public bool LastStep;

    public virtual void Invoke()
    {
        DialogManager.Instance.SetText(Dialog);
    }
}