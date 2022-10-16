using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    public Scene sceneToUnload;


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
       SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Start"));
       // Fadeout
        SceneManager.LoadScene(sceneIndex,LoadSceneMode.Additive);
    }

    public void Quit()
    {
        Application.Quit();
    }
}