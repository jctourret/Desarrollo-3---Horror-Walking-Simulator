using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuManager : MonoBehaviour
{
    public void CloseLayer(GameObject layer)
    {
        layer.SetActive(false);
    }

    public void GoToScene(string scene)
    {
        ScenesLoaderHandler.LoadScene(scene);
    }

    public void GoToSceneWithoutLoading(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ChangeLayer(string name)
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
