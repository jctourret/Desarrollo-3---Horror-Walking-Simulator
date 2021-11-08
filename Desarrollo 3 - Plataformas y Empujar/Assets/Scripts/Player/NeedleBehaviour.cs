using System.Collections;
using UnityEngine;

public class NeedleBehaviour : MonoBehaviour
{
    public int damage = 1;

    //==================================

    [SerializeField] float timeToBlock = 0.1f; /// Se usa para hacerlo kinematico cuando colisiona despues de el tiempo determinado
    [SerializeField] float destroyTime = 6f;

    //==================================

    private void Awake()
    {
        Destroy(this.gameObject, destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.transform.parent = collision.transform;
        this.transform.GetComponent<BoxCollider>().enabled = false;

        StartCoroutine(StopNeedle());

        if (collision.transform.GetComponent<IDamageable>() != null)
            collision.transform.GetComponent<IDamageable>().TakeDamage(damage);
        else
            Debug.Log("No lo detecta");
    }

    IEnumerator StopNeedle()
    {
        yield return new WaitForSeconds(timeToBlock);

        this.transform.GetComponent<Rigidbody>().isKinematic = true;
    }
}
