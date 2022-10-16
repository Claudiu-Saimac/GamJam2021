using UnityEngine;

public class ButtonUnload : MonoBehaviour
{
    public string TargetScene;
    public string UnloadScene;

    public void SceneLoad()
    {
        ScenesManager.LoadScene(TargetScene, UnloadScene);
    }

    public void CloseGame()
    {
        ScenesManager.Quit();
    }
}