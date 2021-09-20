using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour
{
    public void ToGameplay()
    {
        SceneManager.LoadScene("PlatformBehaviour");
    }
}
