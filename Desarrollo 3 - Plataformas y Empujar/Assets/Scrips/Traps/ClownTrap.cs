using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownTrap : MonoBehaviour
{

    public float upForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {

        collision.transform.GetComponent<Rigidbody>().AddForce(Vector3.up * upForce, ForceMode.Impulse);

    }

}
