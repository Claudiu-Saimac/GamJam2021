using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BearAngry : MonoBehaviour
{
    public Image Image;
    private Tween _tween;

    public void OnEnable()
    {
        _tween=  Image.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1).SetLoops(-1);//.SetEase(Ease.InOutBack);
    }
    public void OnDisable()
    {
        if (_tween != null)
            DOTween.Kill(_tween);
    }
    
}
