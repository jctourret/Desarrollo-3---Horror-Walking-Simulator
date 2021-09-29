using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    string[] staticDirections = { "Static N", "Static NE", "Static E", "Static SE", "Static S", "Static SW", "Static W", "Static NW" };
    string[] runDirections = { "Run N", "Run NE", "Run E", "Run SE", "Run S", "Run SW", "Run W", "Run NW" };

    public static Action<GameObject> OnEnemySpawn;
    public Camera cam;
    NavMeshAgent agent;
    Rigidbody rbody;

    [Header("Chase")]
    Vector3 previousPos;
    Vector3 currentPos;
    Animator animator;
    int lastDirection = 0;
    Vector3 north;
    float offsetAngle = 33.3f;

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
        rbody.isKinematic = true;
        OnEnemySpawn?.Invoke(gameObject);
        animator = GetComponentInChildren<Animator>();

        north = new Vector3(Vector3.right.x * Mathf.Cos(Mathf.Deg2Rad * offsetAngle) + Vector3.right.z * Mathf.Sin(Mathf.Deg2Rad * offsetAngle), 0,
            Vector3.right.x * Mathf.Sin(Mathf.Deg2Rad * offsetAngle) + Vector3.right.z * Mathf.Cos(Mathf.Deg2Rad * offsetAngle));
    }
    private void FixedUpdate()
    {
        previousPos = currentPos;
        currentPos = transform.position;
    }

    private void Update()
    {
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

            Vector3 direction = currentPos - previousPos;
            direction = direction.normalized;
            SetDirection(direction);
        }
    }

    private void LateUpdate()
    {
        Vector3 camAdjusted = cam.transform.position;
        camAdjusted.y = 0.0f;
        transform.LookAt(camAdjusted);
    }

    //=======================================
    void SetDirection(Vector3 direction)
    {
        string[] directionArray = null;

        if (direction.magnitude < 0.1f)
        {
            directionArray = staticDirections;
        }
        else
        {
            //directionArray = runDirections;
            directionArray = staticDirections;
            lastDirection = DirectionToIndex(direction, staticDirections.Length);
        }
        animator.Play(directionArray[lastDirection]);
    }

    int DirectionToIndex(Vector3 direction, int states)
    {
        Vector3 normalizedDir = direction.normalized;

        float step = 360f / states;

        float angle = Vector3.SignedAngle(north, normalizedDir, Vector3.up);

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);
    }

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