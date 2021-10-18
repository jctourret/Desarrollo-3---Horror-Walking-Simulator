using UnityEngine;

class FinalLever : InteractiveObject
{
    [Header("Lever")]
    [SerializeField] Animator stair;

    public void ActivateLever()
    {
        stair.SetTrigger("Up");
    }
}
