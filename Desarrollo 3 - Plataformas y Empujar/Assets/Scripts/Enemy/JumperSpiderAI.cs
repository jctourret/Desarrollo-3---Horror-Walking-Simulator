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
    bool ignoreFirst = true;
    bool attack;
    Vector3 launchVelocity;
    Vector3 jumpTarget;
    Vector3 startingPos;
    float radiusLandingOffset = 0.5f;

    private void Start()
    {
        startingPos = transform.position;
        rbody.isKinematic = false;
    }

    void Update()
    {
        float distance;
        if (target != null && isDead == false)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);
            if (!hasAttacked && isGrounded)
            {
                if (distance <= jumpDistance)
                {
                    AkSoundEngine.PostEvent("arana_salta", gameObject);
                    Vector3 landingOffset = transform.position - target.transform.position;
                    landingOffset = landingOffset.normalized * radiusLandingOffset;
                    launchVelocity = initialVelocity(transform.position, target.transform.position + landingOffset, jumpHeight);
                    attack = true;
                }
                else
                {
                    AkSoundEngine.PostEvent("arana_salta", gameObject);

                    Vector3 jumpDirection = target.transform.position - transform.position;
                    jumpTarget = transform.position + (jumpDirection.normalized * jumpDistance);

                    launchVelocity = initialVelocity(transform.position, jumpTarget, jumpHeight);
                    attack = true;
                }
            }
        }
        DetectLanding();
    }
    private void FixedUpdate()
    {
        if (attack)
        {
            rbody.velocity = launchVelocity;
            attack = false;
        }
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
        if (Vector3.Distance(transform.position, target.transform.position)<attackRadius)
        {
            target.GetComponent<IDamageable>().TakeDamage(damage);
        }
        hasAttacked = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Terrain" && !ignoreFirst)
        {
            isGrounded = true;
            rbody.velocity = Vector3.zero;
            JumpAttack();
        }
        else if (ignoreFirst)
        {
            ignoreFirst = false;
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
    }
}
