using System;

class StartLever : InteractiveObject
{
    public static Action ActivateObject;

    public void ActivateLever()
    {
        ActivateObject.Invoke();
    }
}
