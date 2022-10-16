using System;
using UnityEngine;

public class ButtonUnload : MonoBehaviour
{
    public string TargetScene;
    public string UnloadScene;

    private void Awake()
    {
        AudioManager._instance.StopMusic("Tarantella");
    }

    public void SceneLoad()
    {
        ScenesManager.LoadScene(TargetScene, UnloadScene);
    }

    public void CloseGame()
    {
        ScenesManager.Quit();
    }
}