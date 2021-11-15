using System.Collections;
using UnityEngine;

public class ChaserSpiderAI : EnemyAI
{
    [Header("MeleeAttack")]
    [SerializeField] int damage = 1;
    [SerializeField] float meleeDelay = 1;
    [SerializeField] float meleeCooldown = 1;
    [SerializeField] float meleeRange = 3;
    [SerializeField] float meleeConeAngle = 45;

    private float meleeCooldownTimer = 0;

    void Update()
    {
        float distance;
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance <= agent.stoppingDistance)
            {
                if (!hasAttacked)
                {
                    AkSoundEngine.PostEvent("arana_lanza_tela", gameObject);

                    StartCoroutine(Bite(target));
                    hasAttacked = true;
                }
            }
            else
            {
                if (agent.isActiveAndEnabled)
                {
                    agent.SetDestination(target.transform.position);
                }
            }

            if (hasAttacked)
            {
                if (meleeCooldownTimer < meleeCooldown)
                {
                    meleeCooldownTimer += Time.deltaTime;
                }
                else
                {
                    hasAttacked = false;
                    meleeCooldownTimer = 0.0f;
                }
            }
        }
    }


    IEnumerator Bite(GameObject target)
    {
        animator.SetTrigger("Attacked");

        Vector3 startTargetLocation = target.transform.position - transform.position;

        yield return new WaitForSeconds(meleeDelay); // Usando WaitForSeconds se ve mas simple y prolijo

        float angle = Vector3.SignedAngle(startTargetLocation, target.transform.position - transform.position, Vector3.up);
        float distante = Vector3.Distance(transform.position, target.transform.position);

        if (angle < 0)
        {
            angle += 360;
        }
        
        if (angle <= meleeConeAngle && distante <= meleeRange)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }

    // ------------------------------------------

    public void BiteEvent()
    {
        Vector3 startTargetLocation = target.transform.position - transform.position;

        float angle = Vector3.SignedAngle(startTargetLocation, target.transform.position - transform.position, Vector3.up);
        float distante = Vector3.Distance(transform.position, target.transform.position);

        if (angle < 0)
        {
            angle += 360;
        }

        if (angle <= meleeConeAngle && distante <= meleeRange)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
