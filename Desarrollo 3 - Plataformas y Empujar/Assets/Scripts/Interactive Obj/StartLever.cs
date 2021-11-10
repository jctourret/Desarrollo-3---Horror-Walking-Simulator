using System;

class StartLever : InteractiveObject
{
    public static Action ActivateObject;

    public void ActivateLever()
    {
        AkSoundEngine.PostEvent("palanca_torre1", gameObject);

        ActivateObject.Invoke();
    }
}
