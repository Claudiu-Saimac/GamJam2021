using System;

[Serializable]
public class FoodEvent
{
    public string Dialog;

    public bool LastStep;
    public bool NeedRedo;
    public virtual void Invoke()
    {
        DialogManager.Instance.SetText(Dialog);
    }
}