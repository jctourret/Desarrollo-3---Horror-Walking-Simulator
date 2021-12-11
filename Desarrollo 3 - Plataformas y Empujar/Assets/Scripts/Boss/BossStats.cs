using UnityEngine;
using System;

public class BossStats : MonoBehaviour, IDamageable
{
    public static Action<int, int> SetLifeBar;
    public static Action ActivateDeath;

    public GameObject lifeBarPref;
    public GameObject spiderBloodPref;

    public int maxLife = 20;
    public int actualLife = 20;

    private bool alive = true;
    private Animator animator;
    private EnemyAI enemyAI;

    private GameObject activeLifeBar;

    //==================================

    private void Awake()
    {
        animator = transform.parent.GetComponentInChildren<Animator>();
        enemyAI = transform.parent.GetComponentInChildren<EnemyAI>();
    }

    private void Start()
    {
        activeLifeBar = Instantiate(lifeBarPref);
        activeLifeBar.transform.SetParent(GameObject.Find("BossLifeBar").transform, false);
        
        activeLifeBar.SetActive(true);

        actualLife = maxLife;

        SetLifeBar?.Invoke(maxLife, actualLife);

        AkSoundEngine.PostEvent("boss_frase01", gameObject);
    }

    public void TakeDamage(int damage)
    {
        actualLife -= damage;

        SetLifeBar?.Invoke(maxLife, actualLife);

        if (actualLife <= 0)
        {
            Eliminated();
        }
        else
        {
            AkSoundEngine.PostEvent("boss_recibe_dano", gameObject);

            if(actualLife % 10 == 0)
            {
                AkSoundEngine.PostEvent("boss_risas", gameObject);
            }

        }
    }

    public void Eliminated()
    {
        if (!alive)
        {
            Debug.LogError("Eliminacion incorrecta de BOSS: BossStats.cs");
            return;
        }

        ActivateDeath?.Invoke();

        // Create particles:
        GameObject blood = Instantiate(spiderBloodPref, this.transform.position, Quaternion.Euler(Vector3.up));
        Destroy(blood, 5f);
        // ----

        AkSoundEngine.PostEvent("boss_muere", gameObject);

        AkSoundEngine.PostEvent("partida_musica06_victoria", gameObject);

        animator.SetTrigger("IsDead");
        enemyAI.KillSpider(true);

        alive = false;

        activeLifeBar.SetActive(false);

        LoaderManager.Get().SpawnBasicItem(this.transform.position);
    }
}
