using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public static Action<int> OnPlayerDamaged;
    public static Action<int> OnPlayerEarnLive;
    public static Action<int> OnPlayerEarnMaxLive;
    public static Action<int> OnPlayerLoseMaxLive;
    public static Action OnPlayerDamageDeath;

    [Header("Stats")]
    public int maxLives = 6;
    public int lives = 6;

    [Header("Invincibility")]
    [SerializeField]
    float invDuration;

    SpriteRenderer rend;

    bool invulnerable;
    
    //=============================================

    /// <summary>
    /// Testeo de Daño
    void Update()
    {
        /// Cura Vida:
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(1);
            Debug.Log("Se daño Magicamente 1!");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(2);
            Debug.Log("Se daño Magicamente 2!");
        }

        /// Aumenta Dinero:
        if (Input.GetKeyDown(KeyCode.L))
        {
            EarnLive(1);
            Debug.Log("Se curo Magicamente 1!");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            EarnLive(2);
            Debug.Log("Se curo Magicamente 2!");
        }

        /// Aumenta Vida Maxima:
        if (Input.GetKeyDown(KeyCode.M))
        {
            EarnMaxLives(1);
            Debug.Log("Tiene mas Vida Magicamente 1!");
        }

        /// Reduce Vida Maxima:
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoseMaxLife(1);
            Debug.Log("Perdiste Vida Maxima Magicamente 2!");
        }
    }
    /// </summary>
    /// 
    public int GetMaxLives()
    {
        return maxLives;
    }

    public int GetLives()
    {
        return lives;
    }

    //============================================

    public void EarnMaxLives(int score) // Score es el total de corazones que Gana, no las mitades
    {
        maxLives += score * 2;
        OnPlayerEarnMaxLive?.Invoke(score);

        EarnLive(score * 2);
    }

    public void LoseMaxLife(int score) // Score es el total de corazones que Gana, no las mitades
    {
        maxLives -= score * 2;

        if (lives > maxLives)
        {
            lives = maxLives;
        }

        OnPlayerLoseMaxLive?.Invoke(score);
    }

    public bool EarnLive(int score)
    {
        if (lives < maxLives)
        {
            lives += score;
            OnPlayerEarnLive?.Invoke(score);
            return true;
        }
        return false;
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            lives = lives - damage;
            OnPlayerDamaged?.Invoke(damage);
            Eliminated();
            StartCoroutine(invulnerabilityTimer());
        }
    }

    public void Eliminated()
    {
        if (lives <= 0)
        {
            Destroy(gameObject);
            OnPlayerDamageDeath?.Invoke();
        }
    }

    IEnumerator invulnerabilityTimer()
    {
        float elapsed = 0.0f;
        Color originalColor = rend.color; 
        while (elapsed < invDuration)
        {
            if(elapsed % 2 == 0)
            {
                rend.color = Color.clear;
            }
            else
            {
                rend.color = originalColor;
            }
            yield return null;
        }
        invulnerable = false;
    }
}
