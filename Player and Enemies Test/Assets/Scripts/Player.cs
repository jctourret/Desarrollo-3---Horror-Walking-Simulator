using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public static Action<GameObject> OnPlayerLand;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [SerializeField]
    float movementSpeed = 1f;
    float jumpForce = 7.0f;
    [SerializeField]
    bool isGrounded;

    Rigidbody rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        Vector3 currentPos = rbody.position;
        float horizontalInput = -Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(horizontalInput,0.0f,verticalInput);
        input = Vector3.ClampMagnitude(input,1);
        Vector3 movement = input * movementSpeed;
        Vector3 newPos = currentPos + movement * Time.fixedDeltaTime;
        rbody.MovePosition(newPos);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rbody.AddForce(Vector3.up * jumpForce ,ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = true;
            OnPlayerLand?.Invoke(collision.collider.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
