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
        currentTarget = other.GetComponentInParent<PlayerMovement>();
        if (currentTarget != null)
        {
            currentTarget.movementSpeed -= slowStrength;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentTarget != null)
        {
            if (other.GetComponent<PlayerMovement>() == currentTarget)
            {
                currentTarget.movementSpeed += slowStrength;
                currentTarget = null;
            }
        }
    }
}
