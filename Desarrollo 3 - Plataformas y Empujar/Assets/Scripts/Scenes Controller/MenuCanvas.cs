using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour
{
    public void CloseLayer(GameObject layer)
    {
        layer.SetActive(false);
    }

    public void GoToScene(string scene)
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
