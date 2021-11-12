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

    public static Action<int> ShowMoney;

    [Header("Invincibility")]
    [SerializeField] static int playerMoney = 0;

    [Header("Stats")]
    public int maxLives = 6;
    public int lives = 6;

    [Header("Invincibility")]
    [SerializeField]
    float invDuration;
    [SerializeField]
    bool invulnerable = false;
    [SerializeField]
    int flickers = 3;

    SpriteRenderer rend;

    //=============================================

    private void OnEnable()
    {
        Basic_ItemReward.EarnMoney += EarnPlayerMoney;
        MarketSlot.PayItem += LosePlayerMoney;
    }

    private void OnDisable()
    {
        Basic_ItemReward.EarnMoney -= EarnPlayerMoney;
        MarketSlot.PayItem -= LosePlayerMoney;
    }

    private void Awake()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    public int GetMaxLives()
    {
        return maxLives;
    }

    public int GetLives()
    {
        return lives;
    }

    //============================================

    public static int GetPlayerMoney()
    {
        return playerMoney;
    }

    public void EarnPlayerMoney(int money)
    {
        playerMoney += money;

        ShowMoney(GetPlayerMoney());
    }

    public void LosePlayerMoney(int cost)
    {
        playerMoney -= cost;

        ShowMoney(GetPlayerMoney());
    }

    //============================================

    public void EarnMaxLives(int score, bool earnLive) // Score es el total de corazones que Gana, no las mitades
    {
        maxLives += score * 2;
        OnPlayerEarnMaxLive?.Invoke(score);

        if(earnLive)
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
            invulnerable = true;
            StartCoroutine(invulnerabilityTimer());
        }
    }

    public void Eliminated()
    {
        if (lives <= 0)
        {
            AkSoundEngine.PostEvent("muere_personaje", gameObject);
            //Destroy(gameObject);
            //gameObject.SetActive(false);
            OnPlayerDamageDeath?.Invoke();
        }
        else
        {
            if(lives < (maxLives / 2))
                AkSoundEngine.PostEvent("impacto_enemigo", gameObject);
            else
                AkSoundEngine.SetRTPCValue("Player_dañado", lives);
        }
    }

    IEnumerator invulnerabilityTimer()
    {
        float elapsed = 0.0f;
        float flickerTimer = 0.0f;
        Color originalColor = rend.color; 
        while (elapsed < invDuration)
        {
            elapsed += Time.deltaTime;
            flickerTimer += Time.deltaTime;
            if(flickerTimer > invDuration/flickers)
            {
                if (rend.color == originalColor)
                {
                    rend.color = Color.red;
                }
                else
                {
                    rend.color = originalColor;
                }
                flickerTimer = 0.0f;
            }
            yield return null;
        }
        rend.color = originalColor;
        invulnerable = false;
    }
}
