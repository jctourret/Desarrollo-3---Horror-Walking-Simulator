using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTest : MonoBehaviour
{

    float shotHeight = 3f;

    [SerializeField]float spawntime = 0.5f;
    float spawntimer = 0.0f;

    public Transform target;
    [SerializeField] GameObject sphere;
    Transform shotPoint;
    Rigidbody rb;
    
    // Update is called once per frame
    void Update()
    {
        if(spawntimer < spawntime)
        {
            spawntimer += Time.deltaTime;
        }
        else
        {
            GameObject go = Instantiate<GameObject>(sphere,transform.position,Quaternion.identity,null);
            rb = go.GetComponent<Rigidbody>();
            rb.velocity = initialVelocity(transform.position, target.position, shotHeight);
            spawntimer = 0.0f;
        }
    }

    Vector3 initialVelocity(Vector3 origin, Vector3 target, float height)
    {
        //Calculo distancias del tiro oblicuo
        float distanceY = target.y - origin.y;
        Vector3 distanceXZ = new Vector3(target.x - origin.x,
            0.0f, target.z - origin.z);

        //Calculo tiempos del tiro oblicuo
        float timeUp = Mathf.Sqrt(-2 * height / Physics.gravity.y);
        float timeDown = Mathf.Sqrt(2 * (distanceY - height) / Physics.gravity.y);

        //Calculo Velocidades del tiro oblicuo
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * height * Physics.gravity.y);
        Vector3 velocityXZ = distanceXZ / (timeUp + timeDown);

        return velocityY + velocityXZ;
    }
}
