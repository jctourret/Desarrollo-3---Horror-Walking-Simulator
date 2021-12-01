using UnityEngine;

public class WebTrap : MonoBehaviour
{
    [SerializeField]
    float slowStrength = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PlayerMovement>() != null)
        {
            other.gameObject.GetComponentInParent<PlayerMovement>().Slow(slowStrength);
            //Debug.Log(gameObject.name + " has been stepped on.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PlayerMovement>() != null)
        {
            other.gameObject.GetComponentInParent<PlayerMovement>().unSlow(slowStrength);
            //Debug.Log(gameObject.name + " has been exited.");
        }
    }
}
