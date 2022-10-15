using System.Collections;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public const float DialogLifeSpan = 8f;
    public static DialogManager Instance;
    [SerializeField]
    private TextMeshProUGUI _text;

    public GameObject DialogBox;

    public string StartDialog;

    public void Awake()
    {
        Instance = this;

        StartLevelDialog(StartDialog);

    }

    public void SetText(string dialog)
    {
        DialogBox.SetActive(true);
        StopCoroutine(nameof(Dialog));
        StartCoroutine(Dialog(dialog));
        
        CancelInvoke(nameof(CleanText));
        Invoke(nameof(CleanText), DialogLifeSpan);
    }

    private IEnumerator Dialog(string dialog)
    {
        _text.text = string.Empty;

        foreach (var ch in dialog.ToCharArray())
        {
            _text.text += ch;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void CleanText()
    {
        DialogBox.SetActive(false);
        _text.text = string.Empty;
    }

    public void StartLevelDialog(string dialog)
    {
        SetText(dialog);
    }
}
