using System;

class StartLever : InteractiveObject
{
    public static Action ActivateObject;

    public void ActivateLever()
    {
        AkSoundEngine.PostEvent("player_tira_palanca", gameObject);
    }

    public void StartGame()
    {
        ActivateObject?.Invoke();
    }
}
