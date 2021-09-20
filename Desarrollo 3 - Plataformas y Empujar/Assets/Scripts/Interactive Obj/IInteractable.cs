using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractable : MonoBehaviour
{
    float radius = 3;
    Transform interactionTrasform;
    Transform player;

    bool hasInteracted = false;

    public virtual void Display()
    {

    }

    public virtual void Interact()
    {

    }

}
