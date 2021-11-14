using System.Collections;
using UnityEngine;
using System;

public class BossAI : EnemyAI
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
                    StartCoroutine(Swing(target));
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


    IEnumerator Swing(GameObject target)
    {
        //float startTime = Time.time;
        //while (Time.time < startTime + meleeDelay)
        //{
        //    yield return null;
        //}

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

    public void SwingEvent()
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
