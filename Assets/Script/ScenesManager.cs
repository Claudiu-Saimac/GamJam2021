using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    public void LoadScene(int sceneIndex)
    {
     //  Tranzition.
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
       // Fadeout
        SceneManager.LoadScene(sceneIndex);
    }
}