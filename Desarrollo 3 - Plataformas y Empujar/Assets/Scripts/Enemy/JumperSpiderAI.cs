using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperSpiderAI : EnemyAI
{
    [Header("JumpAttack")]
    [SerializeField] float jumpHeight = 3;
    [SerializeField] float jumpDistance = 4;
    [SerializeField] float attackRadius = 2;
    [SerializeField] float jumpCooldown = 1;
    [SerializeField] float jumpCooldownTimer;
    [SerializeField] int damage = 1;

    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        float distance;
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);
            if (!hasAttacked)
            {
                if (distance <= jumpDistance)
                {
                    rbody.velocity = initialVelocity(transform.position, target.transform.position, jumpHeight);
                }
                else
                {
                    Vector3 jumpDirection = transform.position - target.transform.position;
                    Vector3 jumpTarget = jumpDirection.normalized * jumpDistance;

                    rbody.velocity = initialVelocity(transform.position, jumpTarget, jumpHeight);
                }
            }
            else
            {
                if (jumpCooldownTimer < jumpCooldown)
                {
                    jumpCooldownTimer += Time.deltaTime;
                }
                else
                {
                    hasAttacked = false;
                    jumpCooldownTimer = 0.0f;
                }
            }
        }
    }

    void DetectLanding()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1, LayerMask.GetMask()))
        {
            if (!isGrounded)
            {
                JumpAttack();
            }
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void JumpAttack()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, attackRadius, Vector3.zero, 1.0f);
        for(int i = 0; i < hit.Length; i++)
        {
            IDamageable damageable = hit[i].collider.GetComponent<IDamageable>();
            if (damageable != null && hit[i].collider.tag == "Player")
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
