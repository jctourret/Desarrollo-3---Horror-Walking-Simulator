using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebProjectile : MonoBehaviour
{
    [SerializeField] float slowStrength;
    [SerializeField] float slowDuration;
    [SerializeField] GameObject trap;
    [SerializeField] Vector3 velocity;

    PlayerMovement target;
    Rigidbody rb;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        velocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        target = collision.collider.gameObject.GetComponentInParent<PlayerMovement>();
        if(target != null)
        {
            StartCoroutine(target.Slow(slowStrength,slowDuration));
            Destroy(gameObject);
        }
        if(collision.collider.gameObject.tag == "Terrain")
        {
            GameObject go = Instantiate(trap,transform.position, Quaternion.identity, collision.collider.transform);
            Destroy(gameObject);
        }
    }

}
