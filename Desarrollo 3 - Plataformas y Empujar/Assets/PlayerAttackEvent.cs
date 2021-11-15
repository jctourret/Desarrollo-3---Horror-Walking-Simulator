using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEvent : MonoBehaviour
{
    ThrowNeedle behaviour;
    void Start()
    {
        behaviour = GetComponentInParent<ThrowNeedle>();
    }
    public void SpawnNeedle()
    {
        behaviour.SpawnNeedle();
    }
}
