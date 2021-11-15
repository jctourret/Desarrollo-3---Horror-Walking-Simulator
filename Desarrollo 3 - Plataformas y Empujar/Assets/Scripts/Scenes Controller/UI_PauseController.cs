using UnityEngine;

public class UI_PauseController : MonoBehaviour
{
    public string escapeKey = "Pause";
    public GameObject pauseLayer;

    bool isPaused = false;

    private void Update()
    {
        if (Input.GetButtonDown(escapeKey))
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
        AkSoundEngine.PostEvent("Pausa_entrada", gameObject);

        Time.timeScale = 0f;

        pauseLayer.SetActive(true);
    }

    public void ClosePause()
    {
        AkSoundEngine.PostEvent("Pausa_salida", gameObject);

        Time.timeScale = 1f;

        pauseLayer.SetActive(false);
    }

}
