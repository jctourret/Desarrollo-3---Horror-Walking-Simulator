using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    public int life = 10;

    public GameObject spiderBloodPref;

    private Animator animator;
    private bool alive = true;

    private EnemyAI enemyAI;

    private void Awake()
    {
        animator = transform.parent.GetComponentInChildren<Animator>();
        enemyAI = transform.parent.GetComponentInChildren<EnemyAI>();
    }

    //==================================

    public void TakeDamage(int damage)
    {
        life -= damage;
        animator.SetTrigger("Damaged");

        if (life <= 0)
            Eliminated();
        else
            AkSoundEngine.PostEvent("arana_recibe_dano", gameObject);
    }

    public void Eliminated()
    {
        if (!alive)
        {
            Debug.LogError("Eliminacion incorrecta de ARAÑA: EnemyStats.cs");
            return;
        }
        else
        {
            alive = false;

            GetComponent<Collider>().isTrigger = true;

            // Create particles:
            GameObject blood = Instantiate(spiderBloodPref, this.transform.position, Quaternion.Euler(Vector3.up));
            Destroy(blood, 5f);
            // ----

            enemyAI.KillSpider(true);

            animator.SetTrigger("IsDead");

            AkSoundEngine.PostEvent("arana_muere", gameObject);

            Debug.LogWarning("arana_muere");

            LoaderManager.Get().SpawnBasicItem(this.transform.position);
            Destroy(this.transform.parent.gameObject, 5f);
        }
    }

    //==================================

    //public void CallDeathAnimation()    // Esta funcion se va a llamar cuando se haga el final de la animacion de muerte
    //{
    //    Destroy(this.gameObject);
    //}
}
