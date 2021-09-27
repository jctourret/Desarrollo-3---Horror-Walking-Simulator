using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowNeedle : MonoBehaviour
{
    public Transform needleSpawner;
    public GameObject needleObj;

    [Range(0,15)]
    public float throwForce = 10f;

    [Header("Directions of Axis")]
    public Vector3[] directions = new Vector3[4];
    /// Directions:
    /// [0] Up
    /// [1] Down
    /// [2] Right 
    /// [3] Left
    /// -----------
    
    //==================================

    float reloadShoot = 0.5f;
    bool ableToShoot = true;

    float multiplierForce = 100f;

    //==================================

    private void Start()
    {
        ableToShoot = true;
    }

    private void Update()
    {
        ShootInput();
    }

    //=================================

    void ShootInput()
    {
        if (ableToShoot)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                var go = Instantiate(needleObj, needleSpawner.position, Quaternion.Euler(directions[0]));
                go.GetComponent<Rigidbody>().AddForce(needleSpawner.forward * throwForce * multiplierForce, ForceMode.Force);
                StartCoroutine(LoadNeedle());

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                var go = Instantiate(needleObj, needleSpawner.position, Quaternion.Euler(directions[1]));
                go.GetComponent<Rigidbody>().AddForce(-needleSpawner.forward * throwForce * multiplierForce, ForceMode.Force);
                StartCoroutine(LoadNeedle());

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                var go = Instantiate(needleObj, needleSpawner.position, Quaternion.Euler(directions[2]));
                go.GetComponent<Rigidbody>().AddForce(needleSpawner.right * throwForce * multiplierForce, ForceMode.Force);
                StartCoroutine(LoadNeedle());

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                var go = Instantiate(needleObj, needleSpawner.position, Quaternion.Euler(directions[3]));
                go.GetComponent<Rigidbody>().AddForce(-needleSpawner.right * throwForce * multiplierForce, ForceMode.Force);
                StartCoroutine(LoadNeedle());
            }
        }
    }

    IEnumerator LoadNeedle()
    {
        ableToShoot = false;

        yield return new WaitForSeconds(reloadShoot);
        
        ableToShoot = true;
    }
}