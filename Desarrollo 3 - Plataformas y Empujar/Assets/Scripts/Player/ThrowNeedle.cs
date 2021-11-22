using System.Collections;
using UnityEngine;

public class ThrowNeedle : MonoBehaviour
{
    public GameObject needleObj;
    Animator animator;

    [Range(0,15)]
    public float throwForce = 10f;
    Quaternion needleRotation;
    Vector3 needleForce;
    float spawnDelay = 0.1f;
    float spawnDelayTimer;

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
        animator = GetComponentInChildren<Animator>();
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
                needleForce = (Vector3.forward * throwForce * multiplierForce);
                needleRotation = Quaternion.Euler(rotations[0]);
                animator.SetTrigger("Attack");
                StartCoroutine("Fire");
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                needleForce = (Vector3.right * throwForce * multiplierForce);
                needleRotation = Quaternion.Euler(rotations[1]);
                animator.SetTrigger("Attack");
                StartCoroutine("Fire");
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                needleForce = (Vector3.back * throwForce * multiplierForce);
                needleRotation = Quaternion.Euler(rotations[2]);
                animator.SetTrigger("Attack");
                StartCoroutine("Fire");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                needleForce = (Vector3.left * throwForce * multiplierForce);
                needleRotation = Quaternion.Euler(rotations[3]);
                animator.SetTrigger("Attack");
                StartCoroutine("Fire");
            }
        }
        
    }

    public void SpawnNeedle()
    {
        var go = Instantiate(needleObj, this.transform.position,needleRotation);
        go.GetComponent<Rigidbody>().AddForce(needleForce, ForceMode.Force);
        StartCoroutine(LoadNeedle());
    }
    
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnNeedle();
    }

    IEnumerator LoadNeedle()
    {
        AkSoundEngine.PostEvent("player_lanza_aguja", gameObject);

        ableToShoot = false;

        yield return new WaitForSeconds(reloadShoot);
        
        ableToShoot = true;
    }
}