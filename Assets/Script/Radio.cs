using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    public Button RadioButton;

    public Image FirstImage;
    public Image SecondImage;
    public Image ThirdImage;

    private Tween tween;

    private bool _playing = false;

    private void Awake()
    {
        RadioButton.onClick.AddListener(Press);
        Invoke(nameof(StartPlay), 0.5f);
    }

    private void StartPlay()
    {
        StartAnimation();
        AudioManager._instance.MusicSource.volume = 0;
        AudioManager._instance.PlayMusic("Tarantella");
        tween = AudioManager._instance.MusicSource.DOFade(.4f, 1.5f);
    }

    private void Press()
    {
        if (_playing)
        {
            StopAnimation();
            _playing = false;
            AudioManager._instance.PauseMusic();
        }
        else
        {
            _playing = true;

            StartAnimation();
            AudioManager._instance.ContinueMusic();
        }
    }

    private void StartAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(Animation());
    }

    private void StopAnimation()
    {
        StopAllCoroutines();

        FirstImage.gameObject.SetActive(false);
        SecondImage.gameObject.SetActive(false);
        ThirdImage.gameObject.SetActive(false);
    }

    private IEnumerator Animation()
    {
        FirstImage.gameObject.SetActive(true);
        SecondImage.gameObject.SetActive(false);
        ThirdImage.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        FirstImage.gameObject.SetActive(false);
        SecondImage.gameObject.SetActive(true);
        ThirdImage.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        FirstImage.gameObject.SetActive(false);
        SecondImage.gameObject.SetActive(false);
        ThirdImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        StartAnimation();

    }




    private void OnDestroy()
    {
        if (tween != null)
            DOTween.KillAll();
        StopAnimation();
        RadioButton.onClick.RemoveListener(Press);
    }
}
