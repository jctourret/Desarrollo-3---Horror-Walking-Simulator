using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public static Action OnPlayerDamaged;
    public static Action OnPlayerDamageDeath;
    [Header("Stats")]
    public int lives = 3;
    
    //=============================================

    public void TakeDamage(int damage)
    {
        lives = lives - damage;
        OnPlayerDamaged?.Invoke();
        Eliminated();
    }

    public void Eliminated()
    {
        if (lives <= 0)
        {
            Destroy(gameObject);
            OnPlayerDamageDeath?.Invoke();
        }
    }
}
