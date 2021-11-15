using UnityEngine;

public class UI_CallWindow : MonoBehaviour
{
    [SerializeField] private GameObject[] windows;

    public void OpenWindow(int i)
    {
        AkSoundEngine.PostEvent("menu_click_otro", gameObject);

        windows[i].SetActive(true);
    }

    public void CloseWindow(int i)
    {
        AkSoundEngine.PostEvent("menu_click_otro", gameObject);

        windows[i].SetActive(false);
    }
}
