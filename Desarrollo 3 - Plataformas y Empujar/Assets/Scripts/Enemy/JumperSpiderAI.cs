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

    [SerializeField] bool isGrounded;

    private void Start()
    {
        rbody.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance;
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);
            if (!hasAttacked && isGrounded)
            {
                if (distance <= jumpDistance)
                {
                    rbody.velocity = initialVelocity(transform.position, target.transform.position, jumpHeight);
                }
                else
                {
                    AkSoundEngine.PostEvent("arana_salta", gameObject);

                    Vector3 jumpDirection = transform.position - target.transform.position;
                    Vector3 jumpTarget = jumpDirection.normalized * jumpDistance;

                    rbody.velocity = initialVelocity(transform.position, jumpTarget, jumpHeight);
                }
            }
        }
        DetectLanding();
    }

    void DetectLanding()
    {
        if (isGrounded)
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

    void JumpAttack()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, attackRadius, Vector3.zero, 1.0f,LayerMask.GetMask("Enemy"));

        for(int i = 0; i < hit.Length; i++)
        {
            IDamageable damageable = hit[i].collider.GetComponent<IDamageable>();
            if (damageable != null && hit[i].collider.tag == "Player")
            {
                damageable.TakeDamage(damage);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Terrain")
        {
            isGrounded = true;
            rbody.velocity = Vector3.zero;
            JumpAttack();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Terrain" && isGrounded)
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

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Terrain")
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.down);
    }
}
