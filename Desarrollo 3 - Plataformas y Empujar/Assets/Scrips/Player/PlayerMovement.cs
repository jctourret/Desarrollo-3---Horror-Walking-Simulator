using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    string[] staticDirections = {"Static N","Static NE","Static E","Static SE","Static S","Static SW", "Static W", "Static NW"};
    string[] runDirections = { "Run N", "Run NE", "Run E", "Run SE", "Run S", "Run SW", "Run W", "Run NW" };

    public static Action<Vector3> OnPlayerMove;
    public static Func<Camera> RecieveCamera;

    [Header("Player Move")]
    public float speedMovement = 10f;

    [Header("Jump Variables")]
    public float forceJump = 5f;
    public float hightLimit = 2f;

    Rigidbody rig;
    Animator animator;
    new Camera camera;

    int lastDirection;

    //==============================================

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        camera = RecieveCamera?.Invoke();
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

    private void LateUpdate()
    {
        transform.LookAt(camera.transform);
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
        SetDirection(movement);
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

    void SetDirection(Vector3 direction)
    {
        string[] directionArray = null;

        if (direction.magnitude < 0.1f)
        {
            directionArray = staticDirections;
        }
        else
        {
            //directionArray = runDirections;
            directionArray = staticDirections;
            lastDirection = DirectionToIndex(direction,runDirections.Length);
        }
        animator.Play(directionArray[lastDirection]);
    }


    int DirectionToIndex(Vector3 direction, int states)
    {
        Vector3 normalizedDir = direction.normalized;

        float step = 360f / states;

        float angle = Vector3.SignedAngle(Vector3.forward, normalizedDir, Vector3.up);

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);
    }
    //==============================================

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position - new Vector3(0, hightLimit, 0);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, pos);
    }
}
