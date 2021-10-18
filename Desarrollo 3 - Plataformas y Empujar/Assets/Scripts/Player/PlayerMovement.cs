using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static Action<Vector3> OnPlayerMove;
    public static Action OnPlayerFallDeath;

    [Header("Player Move")]
    public float movementSpeed = 10f;
    [SerializeField]
    float dashTime = 1.5f;
    bool controllable;
    [SerializeField]
    Vector3 playerVelocity;
    //[SerializeField]
    bool isGrounded;

    [Header("Gravity")]
    [SerializeField]
    float gravity;
    [SerializeField]
    float fallDeath;

    [Header("Jump Variables")]
    [SerializeField]
    float maxJumpHeight;

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
        animator = GetComponentInChildren<Animator>();
        controllable = true;

        controller.Move(transform.position);

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


    void MoveInput()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (controllable)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            move = Vector3.ClampMagnitude(move, 1);
            float magnitude = move.magnitude;
            animator.SetFloat("Magnitude", magnitude);
            if(magnitude < 0.001)
            {
                animator.SetFloat("lastDirY",move.z);
                animator.SetFloat("lastDirX", move.x);
            }
            else
            {
                animator.SetFloat("Horizontal",move.x);
                animator.SetFloat("Vertical",move.z);
            }

            controller.Move(move * Time.deltaTime * movementSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                playerVelocity.y += Mathf.Sqrt(maxJumpHeight * -3.0f * gravity); // Que es el 3?
            }
            animator.SetBool("IsGrounded", isGrounded);
            playerVelocity.y += gravity * Time.deltaTime;
            if (playerVelocity.y < 0)
            {
                animator.SetBool("Falling",true);
                animator.SetBool("Jumping",false);
            }
            else
            {
                animator.SetBool("Jumping",true);
                animator.SetBool("Falling", false);
            }
            controller.Move(playerVelocity * Time.deltaTime);



            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine(Dash(move));
                controllable = false;
            }
        }
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
