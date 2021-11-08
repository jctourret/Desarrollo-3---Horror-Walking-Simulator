using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTest : MonoBehaviour
{

    float shotHeight = 3f;
    public Transform target;
    Transform shotPoint;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shotPoint = transform;
        //Calculo distancias del tiro oblicuo
        float distanceY = target.transform.position.y - shotPoint.position.y;
        Vector3 distanceXZ = new Vector3(target.transform.position.x - shotPoint.position.x,
            0.0f, target.transform.position.z - shotPoint.position.z);

        //Calculo tiempos del tiro oblicuo
        float timeUp = Mathf.Sqrt(-2 * shotHeight / Physics.gravity.y);
        float timeDown = Mathf.Sqrt(2 * (distanceY - shotHeight) / Physics.gravity.y);

        //Calculo Velocidades del tiro oblicuo
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * shotHeight * Physics.gravity.y);
        Vector3 velocityXZ = distanceXZ / (timeUp + timeDown);

        rb.velocity = velocityY + velocityXZ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
