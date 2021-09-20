using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowNeedle : MonoBehaviour
{

    public Transform needleSpawner;
    public GameObject needleObj;

    [Range(0,15)]
    public float throwForce = 10f;

    //==================================

    float reloadShoot = 0.5f;
    bool ableToShoot = true;

    float multiplierForce = 100f;
    Vector3[] directions = new Vector3[4];

    //==================================

    private void Awake()
    {
        directions[0] = new Vector3(0, 0, 0);   // Up direction
        directions[1] = new Vector3(180, 0, 0); // Down direction
        directions[2] = new Vector3(0, 90, 0);  // Right direction
        directions[3] = new Vector3(0, -90, 0); // Left direction

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