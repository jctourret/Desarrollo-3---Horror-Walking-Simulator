using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public static Func<Camera> RecieveCamera;
    NavMeshAgent agent;
    Rigidbody rbody;
    Camera camera;
    [SerializeField]
    float pushForce = 5f;
    int damage = 1;

    private void OnEnable()
    {
        PlayerMovement.OnPlayerMove += SetDestination;
    }
    private void OnDisable()
    {
        PlayerMovement.OnPlayerMove -= SetDestination;
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rbody = GetComponent<Rigidbody>();
        camera = RecieveCamera?.Invoke();
    }

    private void LateUpdate()
    {
        transform.LookAt(camera.transform);
    }

    void SetDestination(Vector3 destination)
    {
        agent.destination = destination;
    }
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damaged = collision.collider.GetComponent<IDamageable>();
        if (collision.collider.tag == "Player" && damaged !=null)
        {
            Rigidbody playerbody = collision.collider.GetComponent<Rigidbody>();
            Vector3 direction = collision.collider.transform.position - transform.position;
            direction = direction.normalized;

            playerbody.AddForce(direction*pushForce,ForceMode.Impulse);
            damaged.TakeDamage(damage);

            rbody.AddForce(-direction * pushForce, ForceMode.Impulse);
            
            Debug.Log("Pushed Player");
        }
    }
}
