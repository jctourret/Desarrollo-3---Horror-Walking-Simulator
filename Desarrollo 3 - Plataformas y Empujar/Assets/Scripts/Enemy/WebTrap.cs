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
            }
        }
    }
}
