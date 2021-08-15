using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Move")]
    public float speedMovement = 10f;

    [Header("Jump Variables")]
    public float forceJump = 5f;
    public float hightLimit = 2f;
    //public bool isJumping = false;

    Rigidbody rig;

    //==============================================

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }


    void Update()
    {
        PlayerInput();

        PlayerJump();
    }

    //==============================================

    void PlayerInput()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized * speedMovement * Time.deltaTime;

        rig.MovePosition(transform.position + direction);
    }

    void PlayerJump()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(landingRay, out hit, hightLimit))
        {
            if (hit.collider.tag == "Terrain")
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rig.AddForce(transform.up * forceJump, ForceMode.Impulse);
                }
            }
        }
    }

    //==============================================

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position - new Vector3(0, hightLimit, 0);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, pos);
    }
}
