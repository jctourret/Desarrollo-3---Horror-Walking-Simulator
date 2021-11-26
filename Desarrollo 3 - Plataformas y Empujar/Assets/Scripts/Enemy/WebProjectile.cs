using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebProjectile : MonoBehaviour
{
    [SerializeField] float slowStrength;
    [SerializeField] float slowDuration;
    [SerializeField] GameObject trap;
    [SerializeField] Vector3 velocity;

    //PlayerMovement target;
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
        //PlayerMovement target = collision.collider.gameObject.GetComponentInParent<PlayerMovement>();
        
        //if(target != null)
        //{
        //    StartCoroutine(target.Slow(slowStrength, slowDuration));
        //    Destroy(gameObject);
        //}
        //else
        if(collision.collider.gameObject.tag == "Terrain")
        {
            GameObject go = Instantiate(trap,transform.position, Quaternion.identity, collision.collider.transform);

            go.transform.localScale = new Vector3(go.transform.localScale.x / collision.transform.localScale.x,
                                                  go.transform.localScale.y / collision.transform.localScale.y,
                                                  go.transform.localScale.z / collision.transform.localScale.z);

            AkSoundEngine.PostEvent("telarana_abre", gameObject);

            Destroy(gameObject);
        }
    }

}
