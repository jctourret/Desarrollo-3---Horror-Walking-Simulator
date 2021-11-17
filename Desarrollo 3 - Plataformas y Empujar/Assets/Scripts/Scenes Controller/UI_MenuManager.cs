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
        AkSoundEngine.PostEvent("menu_click_play", gameObject);

        ScenesLoaderHandler.LoadScene(scene);
    }

    public void GoToSceneWithoutLoading(string scene)
    {
        AkSoundEngine.PostEvent("menu_click_otro", gameObject);

        SceneManager.LoadScene(scene);
    }

    public void RestartGame(string scene)
    {
        AkSoundEngine.PostEvent("partida_click_replay", gameObject);

        SceneManager.LoadScene(scene);
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
