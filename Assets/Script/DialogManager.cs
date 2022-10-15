using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    [SerializeField]
    private TextMeshProUGUI _text;

    public void Awake()
    {
        Instance = this;
    }

    public void SetText(string dialog)
    {
        _text.text = dialog;
    }

    public void CleanText()
    {
        _text.text = string.Empty;
    }

    public void StartLevelDialog(string dialog)
    {
        _text.text = dialog;
    }
}
