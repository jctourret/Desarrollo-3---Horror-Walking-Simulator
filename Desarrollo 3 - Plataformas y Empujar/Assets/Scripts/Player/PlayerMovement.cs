using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    string[] staticDirections = {"Static N","Static NE","Static E","Static SE","Static S","Static SW", "Static W", "Static NW"};
    string[] runDirections = { "Run N", "Run NE", "Run E", "Run SE", "Run S", "Run SW", "Run W", "Run NW" };
    string[] dashDirections = { "Dash N", "Dash NE", "Dash E", "Dash SE", "Dash S", "Dash SW", "Dash W", "Dash NW"};

    public static Action<Vector3> OnPlayerMove;
    public static Action OnPlayerFallDeath;

    [Header("Player Move")]
    public float speedMovement = 10f;
    [SerializeField]
    int offsetAngle = 45;
    [SerializeField]
    float dashTime = 1.5f;
    bool controllable;

    [Header("Gravity")]
    [SerializeField]
    float fallDeath;
    [SerializeField]
    float groundedGravity;
    [SerializeField]
    float gravity;

    [Header("Jump Variables")]
    [SerializeField]
    float maxJumpHeight;
    [SerializeField]
    float jumpTime;
    [SerializeField]
    float initialJumpVelocity;

    CharacterController controller;
    Animator animator;
    [SerializeField]
    int lastDirection;

    [Header("Interaction")]
    [SerializeField]
    float interactionRadius;
    [SerializeField]
    float displayRadius;

    //==============================================

    void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.Move(transform.position);
        animator = GetComponentInChildren<Animator>();
        controllable = true;

        float timeToApex = jumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex,2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void Update()
    {
        MoveInput();
        OnPlayerMove?.Invoke(transform.position);

        CheckFallDeath();
    }

    //==============================================

    void CheckFallDeath()
    {
        if (transform.position.y < fallDeath)
        {
            OnPlayerFallDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    void InteractionInput()
    {
        IInteractable interactable = null;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (RaycastHit hit in Physics.SphereCastAll(transform.position, interactionRadius, Vector3.zero, LayerMask.GetMask("Interactive")))
            {
                if (interactable == null || Vector3.Distance(interactable.transform.position, transform.position) > Vector3.Distance(hit.transform.position, transform.position))
                {
                    interactable = hit.collider.GetComponent<IInteractable>();
                }
            }
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    void MoveInput()
    {
        float horizontalInput;
        float verticalInput;
        if (controllable)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }
        else
        {
            horizontalInput = 0;
            verticalInput = 0;
        }

        float verticalOffsetInput = verticalInput * Mathf.Cos(offsetAngle) + horizontalInput * Mathf.Sin(offsetAngle);
        float horizontalOffsetInput = -verticalInput * Mathf.Sin(offsetAngle) + horizontalInput * Mathf.Cos(offsetAngle); 

        Vector3 input = new Vector3(verticalOffsetInput, 0.0f, -horizontalOffsetInput);

        input = Vector3.ClampMagnitude(input, 1);
        Vector3 movement = input * speedMovement * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movement = applyGravity(movement);
            StartCoroutine(Dash(movement));
            controllable = false;
        }
        else
        {
            SetDirection(movement);
            movement = applyGravity(movement);

            movement = PlayerJump(movement);

            controller.Move(movement);
        }
    }

    Vector3 applyGravity(Vector3 movement)
    {
        if (controller.isGrounded)
        {
            movement.y = groundedGravity;
        }
        else
        {
            movement.y += gravity * Time.deltaTime;
        }
        return movement;
    }

    Vector3 PlayerJump(Vector3 movement)
    {
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            movement.y = initialJumpVelocity;
        }
        return movement;
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

        Vector3 north = new Vector3(Vector3.right.x * Mathf.Cos(offsetAngle) + Vector3.right.z * Mathf.Sin(offsetAngle), 0, Vector3.right.x * Mathf.Sin(offsetAngle) + Vector3.right.z * Mathf.Cos(offsetAngle));
        float angle = Vector3.SignedAngle(north, normalizedDir, Vector3.up);

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);
    }

    //=============================================


    IEnumerator Dash(Vector3 movement)
    {
        float startTime = Time.time;

        while (Time.time< startTime + dashTime)
        {
            controller.Move(movement*2);
            yield return null;
        }
        controllable = true;
    }


    //==============================================

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position - new Vector3(0, maxJumpHeight, 0);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, pos);
    }
}
