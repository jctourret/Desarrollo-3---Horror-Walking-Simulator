using UnityEngine;

class FinalLever : InteractiveObject
{
    [Header("Lever")]
    [SerializeField] Animator stair;

    public void ActivateLever()
    {
        AkSoundEngine.PostEvent("player_tira_palanca", gameObject);
    }

    public void MoveElevator()
    {
        AkSoundEngine.PostEvent("ascensor_palanca_victoria", gameObject);

        stair.SetTrigger("Up");
    }
}
