using UnityEngine;
using UnityEngine.UI;

public class CuttingBoardSocket : MonoBehaviour
{
    public bool BowlPaced = false;

    public Image BowlImage;

    public Sprite EggInBowlImage;
    public Sprite BowlISprite;

    public ItemHolder BowlHolder;

    public bool CheckForBowl(FoodItem foodItem)
    {
        if (BowlPaced == false)
        {
            if (foodItem.FoodType == FoodTypes.MixingBowl)
            {
                BowlPaced = true;
                BowlImage.sprite = BowlISprite;

                BowlHolder = foodItem.ItemHolder;

                BowlImage.color = new Color(255, 255, 255, 255);

                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    public bool CheckEvolveEgg(FoodItem foodItem,Image image)
    {
        if (BowlPaced)
        {
            foodItem.FoodType = FoodTypes.BowlWithEggs;
            BowlHolder.gameObject.SetActive(true);
            BowlHolder.Reset();

            foodItem.CanEvolve = true;

            PlaySound();

            BowlPaced = false;
            BowlImage.color = new Color(255, 255, 255, 0);
            image.sprite = EggInBowlImage;
            return true;
            
        }

        return false;
    }

    private void PlaySound()
    {
        AudioManager._instance.Play("Prajit");
    }
}
