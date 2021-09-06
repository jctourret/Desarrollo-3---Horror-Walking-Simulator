using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public static Action OnPlayerDamaged;
    public static Action OnPlayerDamageDeath;

    [Header("Stats")]
    public int maxLives = 3;
    public int lives = 3;

    //=============================================

    public void EarnMaxLives(int score)
    {
        maxLives += score;
    }

    public bool EarnLive(int score)
    {
        if (lives < maxLives)
        {
            lives += score;
            return true;
        }
        return false;
    }

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
