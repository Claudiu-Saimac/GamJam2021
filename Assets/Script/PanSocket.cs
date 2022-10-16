using UnityEngine;
using UnityEngine.UI;

public class PanSocket : MonoBehaviour
{
    public bool PanPlaced = false;
    
    public Image PanImage;

    public bool CheckFoodPan(FoodItem foodItem)
    {
        if (PanPlaced == false)
        {
            if (foodItem.FoodType == FoodTypes.Pan)
            {
                PanPlaced = true;
                PanImage.color = new Color(255, 255, 255, 255);

                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    public bool CheckFoodEvolve(FoodItem foodItem)
    {
        if (PanPlaced)
        {
            if (foodItem.FoodType == foodItem.EvolveTypes)
                return false;

            if (foodItem.CanEvolve)
            {
                foodItem.FoodType = foodItem.EvolveTypes;
                PlayFriedSound();
                return true;
            }
        }

        return false;
    }

    private void PlayFriedSound()
    {
        AudioManager._instance.Play("Prajit");
    }
}
