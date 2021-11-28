using System;

class Boss_StartLever : InteractiveObject
{
    public static Action ActivateObject;
    public static Action ActivateFight;

    public void StartLever()
    {
        AkSoundEngine.PostEvent("player_tira_palanca_boss", gameObject);
    }

    public void ActivateBossFight()
    {
        ActivateObject?.Invoke();

        ActivateFight?.Invoke();
    }
}
