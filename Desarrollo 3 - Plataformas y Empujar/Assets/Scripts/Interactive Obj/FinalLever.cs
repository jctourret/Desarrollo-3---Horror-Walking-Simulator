using UnityEngine;

class FinalLever : InteractiveObject
{
    [Header("Lever")]
    [SerializeField] Animator stair;

    [SerializeField] private GameObject player;

    private void Awake()
    {
        player = EnemyManager.instance.player;
    }

    public void ActivateLever()
    {
        AkSoundEngine.PostEvent("player_tira_palanca", gameObject);
    }

    public void MoveElevator()
    {
        player.transform.parent = this.transform;

        AkSoundEngine.PostEvent("ascensor_palanca_victoria", gameObject);

        stair.SetTrigger("Up");
    }
}
