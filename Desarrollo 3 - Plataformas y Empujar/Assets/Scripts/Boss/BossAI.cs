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
    private Vector3 startTargetLocation;
    private bool isAttaking = false;

    new void Update()
    {
        base.Update();
        float distance;

        if (agent != null && target != null && isDead == false)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance <= meleeRange)//agent.stoppingDistance)
            {
                if (!hasAttacked && !isAttaking)
                {
                    animator.SetTrigger("Attacking");
                    StartCoroutine(Attack(target));
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
            
            if (hasAttacked && !isAttaking)
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

        animator.SetFloat("Horizontal",agent.velocity.x);
        animator.SetFloat("Vertical", agent.velocity.z);
        animator.SetFloat("Magnitude", agent.velocity.magnitude);
    }


    IEnumerator Attack(GameObject target)
    {
        isAttaking = true;

        Aim();

        yield return new WaitForSeconds(meleeDelay); // Usando WaitForSeconds se ve mas simple y prolijo

        isAttaking = false;

        Swing();
    }

    // ------------------------------------------
    public void Aim()
    {
        startTargetLocation = target.transform.position - transform.position;
    }

    public void Swing()
    {
        AkSoundEngine.PostEvent("boss_ataca_cuchillo", gameObject);
        
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
