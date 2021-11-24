using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    public int life = 10;

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
            GetComponent<Collider>().enabled = false;

            enemyAI.KillSpider(true);

            animator.SetTrigger("IsDead");

            AkSoundEngine.PostEvent("arana_muere", gameObject);

            alive = false;

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
