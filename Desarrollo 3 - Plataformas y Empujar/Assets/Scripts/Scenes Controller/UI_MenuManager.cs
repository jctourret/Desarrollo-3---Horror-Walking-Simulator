using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuManager : MonoBehaviour
{
    public void CloseLayer(GameObject layer)
    {
        AkSoundEngine.PostEvent("menu_click_otro", gameObject);

        layer.SetActive(false);
    }

    public void GoToScene(string scene)
    {
        ScenesLoaderHandler.LoadScene(scene);

        AkSoundEngine.PostEvent("menu_click_play", gameObject);
    }

    public void GoToSceneWithoutLoading(string scene)
    {
        SceneManager.LoadScene(scene);

        AkSoundEngine.PostEvent("menu_click_otro", gameObject);
    }

    public void ChangeLayer(string name)
    {

    }

    public void ExitGame()
    {
        AkSoundEngine.PostEvent("menu_click_exit", gameObject);

        Application.Quit();
    }
}
