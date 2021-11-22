using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebTrap : MonoBehaviour
{
    PlayerMovement currentTarget;
    [SerializeField]
    float slowStrength = 10;

    private void OnTriggerEnter(Collider other)
    {
        currentTarget = other.gameObject.GetComponentInParent<PlayerMovement>();
        if (currentTarget != null)
        {
            currentTarget.Slow(slowStrength);
            Debug.Log(gameObject.name + " has been stepped on.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentTarget != null)
        {
            if (other.gameObject.GetComponentInParent<PlayerMovement>() == currentTarget)
            {
                currentTarget.unSlow(slowStrength);
                currentTarget = null;
                Debug.Log(gameObject.name + " has been exited.");
            }
        }
    }
}
