using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserSpiderAI : EnemyAI
{
    [Header("MeleeAttack")]
    [SerializeField] int damage = 1;
    [SerializeField] float meleeDelay = 1;
    [SerializeField] float meleeCooldown = 1;
    [SerializeField] float meleeCooldownTimer;
    [SerializeField] float meleeRange = 3;
    [SerializeField] float meleeConeAngle = 45;

    // Update is called once per frame
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
        float startTime = Time.time;

        Vector3 startTargetLocation = target.transform.position - transform.position;
        while (Time.time < startTime + meleeDelay)
        {
            yield return null;
        }
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
