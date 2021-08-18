using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rbody;
    [SerializeField]
    float pushForce = 5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        agent.destination = Player.instance.transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Rigidbody playerbody = collision.collider.GetComponent<Rigidbody>();
            Vector3 direction = collision.collider.transform.position - transform.position;
            direction = direction.normalized;
            playerbody.AddForce(direction*pushForce,ForceMode.Impulse);
            rbody.AddForce(-direction * pushForce, ForceMode.Impulse);
            Debug.Log("Pushed Player");
        }
    }
}
