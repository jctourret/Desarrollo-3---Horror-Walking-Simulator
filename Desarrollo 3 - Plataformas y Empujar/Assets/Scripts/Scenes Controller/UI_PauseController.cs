using UnityEngine;

public class UI_PauseController : MonoBehaviour
{
    public string escapeKey = "Pause";
    public GameObject pauseLayer;

    bool isPaused = false;

    private void Update()
    {
        if (Input.GetButtonDown(escapeKey))//Input.GetAxisRaw(escapeKey) >= 1)
        {
            if (!isPaused)
                StartPause();
            else
                ClosePause();
            
            isPaused = !isPaused;
        }
    }

    public void StartPause()
    {
        Time.timeScale = 0f;

        pauseLayer.SetActive(true);
    }

    public void ClosePause()
    {
        Time.timeScale = 1f;

        pauseLayer.SetActive(false);
    }

}
