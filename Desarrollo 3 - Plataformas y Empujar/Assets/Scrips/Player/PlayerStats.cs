using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    public float life = 100f;
    
    //=============================================

    public void TakeDamage(int damage)
    {

    }

    public void Eliminated()
    {

    }
}
