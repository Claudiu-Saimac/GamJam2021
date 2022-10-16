using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSecondScene : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        SceneManager.LoadScene("Start",LoadSceneMode.Additive);
       
    }
}
