using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserSpiderAttack : MonoBehaviour
{

    private ChaserSpiderAI behaviour;

    private void Awake()
    {
        behaviour = GetComponentInParent<ChaserSpiderAI>();
    }

    public void Aim()
    {
        behaviour.Aim();
    }

    public void Bite()
    {
        behaviour.BiteEvent();
    }

}
