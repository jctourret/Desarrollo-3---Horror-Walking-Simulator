using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    string[] staticDirections = {"Static N","Static NE","Static E","Static SE","Static S","Static SW", "Static W", "Static NW"};
    string[] runDirections = { "Run N", "Run NE", "Run E", "Run SE", "Run S", "Run SW", "Run W", "Run NW" };

    public static Action<Vector3> OnPlayerMove;
    public static Action OnPlayerFallDeath;

    [Header("Player Move")]
    public float speedMovement = 10f;

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

    int lastDirection;

    //==============================================

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        float timeToApex = jumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex,2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void Update()
    {
        PlayerInput();
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

    void PlayerInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(verticalInput, 0.0f, -horizontalInput);
        input = Vector3.ClampMagnitude(input, 1);
        Vector3 movement = input * speedMovement * Time.deltaTime;
        SetDirection(movement);

        movement = applyGravity(movement);
        movement = PlayerJump(movement);

        controller.Move(movement);
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
        Vector3 pos = transform.position - new Vector3(0, maxJumpHeight, 0);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, pos);
    }
}
