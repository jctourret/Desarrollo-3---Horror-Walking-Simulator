using UnityEngine;
using UnityEngine.UI;

public class UI_SettingsFrames : MonoBehaviour
{
    public GameObject[] settingsFrames;

    private void Start()
    {
        for (int i = 0; i < settingsFrames.Length; i++)
        {
            if (i == 0)
            {
                settingsFrames[i].SetActive(true);

            }
            else
            {
                settingsFrames[i].SetActive(false);
            }
        }
    }

    public void SetActualFrame(int frame)
    {
        for (int i = 0; i < settingsFrames.Length; i++)
        {
            if(i == frame)
            {
                settingsFrames[frame].SetActive(true);
            }
            else
            {
                settingsFrames[i].SetActive(false);
            }
        }
    }
}
