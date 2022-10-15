using System;

[Serializable]
public class FoodEvent
{
    public string Dialog;

    public bool LastStep;
    public bool NeedRedo;

    public BearState State = BearState.Normal;
    public virtual void Invoke()
    {
        DialogManager.Instance.SetFaceState(State);

        DialogManager.Instance.SetText(Dialog);
    }

    public enum BearState
    {
        Happy,Angry,Normal
    }
}