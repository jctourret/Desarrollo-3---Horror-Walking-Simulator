using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static Action<Vector3> OnPlayerMove;

    [Header("Player Move")]
    public float speedMovement = 10f;

    [Header("Jump Variables")]
    public float forceJump = 5f;
    public float hightLimit = 2f;

    Rigidbody rig;

    //==============================================

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        PlayerInput();
        OnPlayerMove?.Invoke(transform.position);
    }

    private void Update()
    {
        PlayerJump();
    }

    //==============================================

    void PlayerInput()
    {
        Vector3 currentPos = rig.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(verticalInput, 0.0f, -horizontalInput);
        input = Vector3.ClampMagnitude(input, 1);
        Vector3 movement = input * speedMovement;
        Vector3 newPos = currentPos + movement * Time.fixedDeltaTime;
        rig.MovePosition(newPos);
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
