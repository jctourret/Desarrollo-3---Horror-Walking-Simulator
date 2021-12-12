using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static Action<Vector3> OnPlayerMove;
    public static Action OnPlayerFallDeath;

    [Header("Player Move")]
    [SerializeField] float currentSpeed = 10f;
    [SerializeField] float normalSpeed = 10f;
    [SerializeField] float minSpeed = 2;
    [SerializeField] private Vector3 playerVelocity;

    private bool isSlowed = false;
    private bool controllable;
    private bool isGrounded;
    private bool inAir = false;
    bool lastDirRecorded;
    bool right;
    bool up;

    [Header("Gravity")]
    [SerializeField] private float gravity;
    [SerializeField] private float fallDeath;

    [Header("Jump Variables")]
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private int lastDirection;

    [Header("Interaction")]
    [SerializeField] private float interactionRadius;
    [SerializeField] private float displayRadius;

    private CharacterController controller;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private PlayerStats player;

    //==============================================

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GetComponent<PlayerStats>();
        controllable = false;

        //controller.Move(transform.position);
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
        if (transform.position.y < fallDeath && controllable)
        {
            OnPlayerFallDeath?.Invoke();
            AkSoundEngine.PostEvent("player_cae_de_pilar", gameObject);

            PlayerStats.OnPlayerDamageDeath?.Invoke();

            gameObject.SetActive(false);

            controllable = false;

            //player.TakeDamage(player.GetLives());
        }
    }

    void MoveInput()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;

            if (inAir)
            {
                inAir = false;
                AkSoundEngine.PostEvent("player_aterriza", gameObject);
            }
        }
        else
        {
            inAir = true;
        }

        if (controllable && !player.isDead)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            move = Vector3.ClampMagnitude(move, 1);
            float magnitude = move.magnitude;
            animator.SetFloat("Magnitude", magnitude);

            if(magnitude < 0.1)
            {
                if (!lastDirRecorded)
                {
                    animator.SetFloat("lastDirX", move.x);
                    animator.SetFloat("lastDirY", move.z);
                    lastDirRecorded = true;
                }
            }
            else
            {
                animator.SetFloat("Horizontal",move.x);
                animator.SetFloat("Vertical",move.z);
                lastDirRecorded = false;
            }

            right = move.x <= 0;
            up = move.z > 0;

            if(right && up) // right up
            {
                spriteRenderer.flipX = false;
            }
            else if (right && !up) // right down
            {
                spriteRenderer.flipX = true;
            }
            else if (!right && up) //left up
            {
                spriteRenderer.flipX = true;
            }
            else if(!right && !up) //left down
            {
                spriteRenderer.flipX = false;
            }

            controller.Move(move * Time.deltaTime * currentSpeed);


            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                playerVelocity.y += Mathf.Sqrt(maxJumpHeight * -3.0f * gravity); // Que es el 3?
                AkSoundEngine.PostEvent("player_salto", gameObject);
            }

            animator.SetBool("IsGrounded", isGrounded);
            playerVelocity.y += gravity * Time.deltaTime;

            if (playerVelocity.y < 0)
            {
                animator.SetBool("Falling", true);
                animator.SetBool("Jumping", false);
            }
            else
            {
                animator.SetBool("Falling", false);
                animator.SetBool("Jumping", true);
            }
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }

    public void Slow(float slowStrength)
    {
        if(currentSpeed > minSpeed && !isSlowed)
        {
            currentSpeed -= slowStrength;
            isSlowed = true;
            //Debug.Log(gameObject.name + "is slowed");
        }
    }

    public void unSlow(float slowStrength)
    {
        if(currentSpeed < normalSpeed && isSlowed)
        {
            currentSpeed += slowStrength;
            isSlowed = false;
            //Debug.Log(gameObject.name + "is no longer slowed");
        }
    }

    //=============================================

    public void ActivatePlayer()
    {
        controllable = true;
    }

    public IEnumerator Slow(float slowStrength, float slowDuration)
    {
        if(!isSlowed)
        {
            if (currentSpeed > minSpeed)
            {
                currentSpeed -= slowStrength;

                isSlowed = true;
            }

            yield return new WaitForSeconds(slowDuration);
            
            if(currentSpeed < normalSpeed)
            {
                currentSpeed += slowStrength;

                isSlowed = false;
            }
        }

        yield return null;
    }

    //==============================================

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position - new Vector3(0, maxJumpHeight, 0);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, pos);
    }
}
