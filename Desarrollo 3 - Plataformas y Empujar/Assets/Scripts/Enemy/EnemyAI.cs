using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{   
    public new Camera camera;
    int damage = 1;
    NavMeshAgent agent;
    Rigidbody rbody;

    GameObject target;
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
        rbody = GetComponent<Rigidbody>();
        rbody.isKinematic = true;
    }

    private void LateUpdate()
    {
        transform.LookAt(camera.transform);
        if (agent.isActiveAndEnabled)
        {
            agent.destination = target.transform.position;
        }
    }

    //=======================================
    void GetCamera(Camera newCamera)
    {
        camera = newCamera;
    }
    public void pilarFalls()
    {
        agent.enabled = false;
        rbody.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damaged = collision.collider.GetComponent<IDamageable>();
        if (collision.collider.gameObject == target && damaged != null)
        {
            damaged.TakeDamage(damage);
        }
    }
}
