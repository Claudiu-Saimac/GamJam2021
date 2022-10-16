using System.Collections;
using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogManager : MonoBehaviour
{
    public const float DialogLifeSpan = 2f;
    public static DialogManager Instance;
    [SerializeField] private TextMeshProUGUI _text;

    public GameObject DialogBox;

    public GameObject NormalFace;
    public GameObject AngryFace;
    public GameObject HappyFace;

    public string StartDialog;

    private GameObject _target;

    public void Awake()
    {
        Instance = this;
        _target = NormalFace;

        Invoke(nameof(StartLevelDialog), 1f);
    }

    public void SetFaceState(FoodEvent.BearState state)
    {
        int i;
        string quote = "Quote";

        switch (state)
        {
            case FoodEvent.BearState.Happy:
                _target = HappyFace;

                HappyFace.SetActive(true);
                AngryFace.SetActive(false);
                NormalFace.SetActive(false);
                i = Random.Range(1, 2);
                quote = quote + ("Happy" + i.ToString());
                AudioManager._instance.Play(quote);
                break;
            case FoodEvent.BearState.Angry:
                _target = AngryFace;
                i = Random.Range(1, 2);
                quote = quote + ("Angry" + i.ToString());
                AngryFace.SetActive(true);
                HappyFace.SetActive(false);
                NormalFace.SetActive(false);
                AudioManager._instance.Play(quote);
                break;
            case FoodEvent.BearState.Normal:
                _target = NormalFace;
                i = Random.Range(1, 3);
                quote = quote + ("Normal" + i.ToString());
                NormalFace.SetActive(true);
                HappyFace.SetActive(false);
                AngryFace.SetActive(false);
                AudioManager._instance.Play(quote);
                break;
        }

    }

    public void SetText(string dialog)
    {
        DialogBox.SetActive(true);

        CancelInvoke(nameof(CleanText));
        StopCoroutine(nameof(Dialog));
        StartCoroutine(Dialog(dialog));
    }

    private IEnumerator Dialog(string dialog)
    {
        StartAnimation();

        _text.text = string.Empty;

        foreach (var ch in dialog.ToCharArray())
        {
            _text.text += ch;
            yield return new WaitForSeconds(0.05f);
        }

        StopAnimation();

        CancelInvoke(nameof(CleanText));
        Invoke(nameof(CleanText), DialogLifeSpan);
    }

    private void StartAnimation()
    {
        DOTween.Kill(_target.transform);

        _target.transform.DOLocalMoveX(-6, 0.3f).onComplete += () =>
        {
            _target.transform.DOLocalMoveX(6, 0.6f).SetLoops(-1, LoopType.Yoyo);
        };
    }


    private void StopAnimation()
    {
        DOTween.Kill(_target.transform);
        _target.transform.DOLocalMoveX(0, 0.3f);
    }

    public void CleanText()
    {
        DialogBox.SetActive(false);
        _text.text = string.Empty;
    }

    public void StartLevelDialog()
    {
        int i = Random.Range(1, 3);
        string quote = ("QuoteNormal" + i.ToString());
        AudioManager._instance.Play(quote);
        SetText(StartDialog);
    }
}