using UnityEngine;

public class PlayerSoundsAnim : MonoBehaviour
{
    
    public void FootStepSound()
    {
        AkSoundEngine.PostEvent("player_pasos", gameObject);
    }

}
