using UnityEngine;

public class UI_CallWindow : MonoBehaviour
{
    [SerializeField] private GameObject[] windows;

    public void OpenWindow(int i)
    {
        windows[i].SetActive(true);
    }

    public void CloseWindow(int i)
    {
        windows[i].SetActive(false);
    }
}
