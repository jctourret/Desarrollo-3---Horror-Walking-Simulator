using System.Collections;
using UnityEngine;

public class ThrowNeedle : MonoBehaviour
{
    public GameObject needleObj;

    [Range(0,15)]
    public float throwForce = 10f;

    [Header("Directions of Axis")]
    public Vector3[] rotations = new Vector3[4];
    /// Directions:
    /// [0] Up
    /// [1] Right 
    /// [2] Down
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
                var go = Instantiate(needleObj, this.transform.position, Quaternion.Euler(rotations[0]));
                go.GetComponent<Rigidbody>().AddForce(Vector3.forward * throwForce * multiplierForce, ForceMode.Force);
                StartCoroutine(LoadNeedle());

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                var go = Instantiate(needleObj, this.transform.position, Quaternion.Euler(rotations[1]));
                go.GetComponent<Rigidbody>().AddForce(Vector3.right * throwForce * multiplierForce, ForceMode.Force);
                StartCoroutine(LoadNeedle());

            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                var go = Instantiate(needleObj, this.transform.position, Quaternion.Euler(rotations[2]));
                go.GetComponent<Rigidbody>().AddForce(-Vector3.forward * throwForce * multiplierForce, ForceMode.Force);
                StartCoroutine(LoadNeedle());

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                var go = Instantiate(needleObj, this.transform.position, Quaternion.Euler(rotations[3]));
                go.GetComponent<Rigidbody>().AddForce(-Vector3.right * throwForce * multiplierForce, ForceMode.Force);
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