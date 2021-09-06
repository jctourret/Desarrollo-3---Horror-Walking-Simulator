using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleBehaviour : MonoBehaviour
{

    public int damage = 1;

    //==================================

    Rigidbody rig;
    float timeToBlock = 0.15f; /// Se usa para hacerlo kinematico cuando colisiona despues de el tiempo determinado
                               /// Posible idea: Usar el FixedJoin para unir o pegar la aguja con otro objeto
    float destroyTime = 5f;

    //==================================

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();

        Destroy(this.gameObject, destroyTime * 2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(StopNeedle());

        if (collision.transform.GetComponent<Rigidbody>() != null)
        {
            this.gameObject.AddComponent<FixedJoint>();

            GetComponent<FixedJoint>().connectedBody = collision.transform.GetComponent<Rigidbody>();
        }

        if (collision.transform.GetComponent<IDamageable>() != null)
        {
            collision.transform.GetComponent<IDamageable>().TakeDamage(damage);
        }

        Destroy(this.gameObject, destroyTime);
    }

    IEnumerator StopNeedle()
    {
        yield return new WaitForSeconds(timeToBlock);

        GetComponent<BoxCollider>().enabled = false;
        rig.isKinematic = true;
    }

}
