using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public static Action<GameObject> OnEnemySpawn;
    public Camera cam;
    protected NavMeshAgent agent;
    Animator animator;
    Collider coll;

    [SerializeField] protected Rigidbody rbody;
    [SerializeField] protected bool hasAttacked;
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
        animator = GetComponentInChildren<Animator>();
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
        rbody = GetComponent<Rigidbody>();
        rbody.isKinematic = true;
        OnEnemySpawn?.Invoke(gameObject);
    }

    private void Update()
    {
        animator.SetFloat("Horizontal",agent.velocity.x);
        animator.SetFloat("Vertical", agent.velocity.z);
        animator.SetFloat("Magnitude",agent.velocity.magnitude);
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

    protected Vector3 initialVelocity(Vector3 origin, Vector3 target, float height)
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
    public void Fall()
    {
        if(agent != null)
        {
            agent.enabled = false;
        }
        if(rbody != null)
        {
            rbody.isKinematic = false;
        }
    }
}
