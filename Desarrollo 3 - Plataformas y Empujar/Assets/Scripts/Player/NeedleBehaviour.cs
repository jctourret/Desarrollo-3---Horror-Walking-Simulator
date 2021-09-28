using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleBehaviour : MonoBehaviour
{

    public int damage = 1;

    //==================================

    Rigidbody rig;
    [SerializeField] float timeToBlock = 0.1f; /// Se usa para hacerlo kinematico cuando colisiona despues de el tiempo determinado
                                               /// Posible idea: Usar el FixedJoin para unir o pegar la aguja con otro objeto
    [SerializeField] float destroyTime = 5f;

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

            this.transform.parent = collision.transform;
        }

        if (collision.transform.GetComponent<IDamageable>() != null)
        {
            collision.transform.GetComponent<IDamageable>().TakeDamage(damage);

            this.transform.GetComponent<BoxCollider>().enabled = false;
        }

        Destroy(this.gameObject, destroyTime);
    }

    IEnumerator StopNeedle()
    {
        yield return new WaitForSeconds(timeToBlock);

        rig.isKinematic = true;
    }

}
