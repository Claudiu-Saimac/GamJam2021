using System;

[Serializable]
public class FoodEvent
{
    public string Dialog;

    public bool LastStep;
    public bool NeedRedo;
    public bool Pol = false;

    public BearState State = BearState.Normal;
    public virtual void Invoke()
    {
        if (Pol)
        {
            AudioManager._instance.Play("Pol");
            DialogManager.Instance.SetFaceState(State, false);
        }
        else
            DialogManager.Instance.SetFaceState(State);

        DialogManager.Instance.SetText(Dialog);
    }

    public enum BearState
    {
        Happy, Angry, Normal
    }
}