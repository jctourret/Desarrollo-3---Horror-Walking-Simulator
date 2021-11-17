using UnityEngine;

public class UI_DeathLayer : MonoBehaviour
{
    public void StartFinalPause()
    {
        Time.timeScale = 0f;
    }

    public void CloseFinalPause()
    {
        Time.timeScale = 1f;
    }
}
