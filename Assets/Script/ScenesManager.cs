using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }


    public static void LoadScene(string target,string unloadTarget)
    {
        SceneManager.UnloadSceneAsync(unloadTarget);
       
        SceneManager.LoadScene(target, LoadSceneMode.Additive);
    }

    public static void Quit()
    {
        Application.Quit();
    }
}