using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    enum AttackBehaviour { Ranged, Melee, Jumper }

    public static Action<GameObject> OnEnemySpawn;
    public Camera cam;
    NavMeshAgent agent;
    Rigidbody rbody;
    Animator animator;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform shotPoint;
    [SerializeField] AttackBehaviour currentBehaviour;

    [Header("JumpAttack")]
    [SerializeField]float jumpHeight = 3;
    [SerializeField]float jumpDistance = 4;
    [SerializeField]float jumpCooldown;
    [SerializeField]float jumpCooldownTimer;

    [Header("RangedAttack")]
    [SerializeField] float rangedDelay;
    [SerializeField] float rangedCooldown;
    [SerializeField] float rangedCooldownTimer;
    [SerializeField] float rangedRange;
    [SerializeField] float shotHeight = 1;

    [Header("MeleeAttack")]
    [SerializeField] int damage = 1;
    [SerializeField] float meleeDelay;
    [SerializeField] float meleeCooldown;
    [SerializeField] float meleeCooldownTimer;
    [SerializeField] float meleeRange;
    [SerializeField] float meleeConeAngle;
    [SerializeField] bool hasAttacked;
    [SerializeField] bool targetInAttackRange;

    public GameObject target;
    //======================================

    private void OnEnable()
    {
        CameraBehaviour.OnSendCamera += GetCamera;
    }

    private void OnDisable()
    {
        CameraBehaviour.OnSendCamera -= GetCamera;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rbody = GetComponentInChildren<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        rbody.isKinematic = true;
        OnEnemySpawn?.Invoke(gameObject);
        currentBehaviour = (AttackBehaviour)UnityEngine.Random.Range(0, Enum.GetValues(typeof(AttackBehaviour)).Length);
        if(currentBehaviour == AttackBehaviour.Jumper)
        {
            agent.enabled = false;
            rbody.isKinematic = false;
        }
    }

    private void Update()
    {
        animator.SetFloat("Horizontal",agent.velocity.x);
        animator.SetFloat("Vertical", agent.velocity.z);
        animator.SetFloat("Magnitude",agent.velocity.magnitude);
        switch (currentBehaviour)
        {
            case AttackBehaviour.Ranged:
                RangedAttack();
                break;
            case AttackBehaviour.Melee:
                MeleeAttack();
                break;
            case AttackBehaviour.Jumper:
                JumpAttack();
                break;
        }
    }

    private void LateUpdate()
    {
        Vector3 camAdjusted = cam.transform.position;
        camAdjusted.y = 0.0f;
        transform.LookAt(camAdjusted);
    }

    //=======================================

    void GetCamera(Camera newCamera)
    {
        cam = newCamera;
    }

    void JumpAttack()
    {
        float distance;
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance <= jumpDistance)
            {
                rbody.velocity = initialVelocity(transform.position,target.transform.position,jumpHeight);
            }
            else
            {
                Vector3 jumpDirection = transform.position - target.transform.position;
                Vector3 jumpTarget = jumpDirection.normalized * jumpDistance;

                rbody.velocity = initialVelocity(transform.position,jumpTarget,jumpHeight);
            }
            if (hasAttacked)
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

    void RangedAttack()
    {
        float distance;
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance <= rangedRange)
            {
                if (!hasAttacked)
                {
                    GameObject go = Instantiate(projectile, shotPoint.position, Quaternion.identity, null);

                    rbody.velocity = initialVelocity(shotPoint.position,target.transform.position,shotHeight);

                    agent.isStopped = true;
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
                if (rangedCooldownTimer < rangedCooldown)
                {
                    rangedCooldownTimer += Time.deltaTime;
                }
                else
                {
                    hasAttacked = false;
                    rangedCooldownTimer = 0.0f;
                }
            }
        }
    }

    void MeleeAttack()
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

    Vector3 initialVelocity(Vector3 origin, Vector3 target, float height)
    {
        //Calculo distancias del tiro oblicuo
        float distanceY = target.y - origin.y;
        Vector3 distanceXZ = new Vector3(target.x - origin.x,
            0.0f, target.z - origin.z);

        //Calculo tiempos del tiro oblicuo
        float timeUp = Mathf.Sqrt(-2 * height / Physics.gravity.y);
        float timeDown = Mathf.Sqrt(2 * (distanceY - height) / Physics.gravity.y);

        //Calculo Velocidades del tiro oblicuo
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * height * Physics.gravity.y);
        Vector3 velocityXZ = distanceXZ / (timeUp + timeDown);

        return velocityY + velocityXZ;
    }
    public void pilarFalls()
    {
        agent.enabled = false;
        rbody.isKinematic = false;
    }
    IEnumerator Bite(GameObject target)
    {
        float startTime = Time.time;

        Vector3 startTargetLocation = target.transform.position - transform.position;
        while(Time.time < startTime + meleeDelay)
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
