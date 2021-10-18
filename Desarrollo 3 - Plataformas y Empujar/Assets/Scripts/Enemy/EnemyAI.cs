using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public static Action<GameObject> OnEnemySpawn;
    public Camera cam;
    NavMeshAgent agent;
    Rigidbody rbody;
    Animator animator;

    [Header("Attack")]
    [SerializeField]
    int damage = 1;
    [SerializeField]
    float attackDelay;
    [SerializeField]
    float attackCooldown;
    [SerializeField]
    float attackCooldownTimer;
    [SerializeField]
    float attackRange;
    [SerializeField]
    float attackConeAngle;
    [SerializeField]
    bool hasAttacked;
    [SerializeField]
    bool targetInAttackRange;

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

    }

    private void Update()
    {
        animator.SetFloat("Horizontal",agent.velocity.x);
        animator.SetFloat("Vertical", agent.velocity.z);
        animator.SetFloat("Magnitude",agent.velocity.magnitude);
        float distance;
        if (target != null)
        { 
            distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance <= agent.stoppingDistance)
            {
                if (!hasAttacked)
                {
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

            if (hasAttacked)
            {
                if (attackCooldownTimer < attackCooldown)
                {
                    attackCooldownTimer += Time.deltaTime;
                }
                else
                {
                    hasAttacked = false;
                    attackCooldownTimer = 0.0f;
                }
            }

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
    public void pilarFalls()
    {
        agent.enabled = false;
        rbody.isKinematic = false;
    }
    IEnumerator Attack(GameObject target)
    {
        float startTime = Time.time;

        Vector3 startTargetLocation = target.transform.position - transform.position;
        while(Time.time < startTime + attackDelay)
        {
            yield return null;
        }
        float angle = Vector3.SignedAngle(startTargetLocation, target.transform.position - transform.position, Vector3.up);
        float distante = Vector3.Distance(transform.position, target.transform.position);
        if (angle < 0)
        {
            angle += 360;
        }
        if (angle <= attackConeAngle && distante <= attackRange)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
