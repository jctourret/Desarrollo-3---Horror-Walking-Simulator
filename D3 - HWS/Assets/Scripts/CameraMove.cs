using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivity = 500.0f;

    public Transform playerBody;

    float xRotation = 0.0f;
    float viewArc = 90.0f;

    float interactionRange = 5.0f;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-viewArc,viewArc);

        transform.localRotation = Quaternion.Euler(xRotation,0.0f,0.0f);
        playerBody.Rotate(Vector3.up * mouseX );
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
            {
                Iinteractible interactible = hit.collider.GetComponent<Iinteractible>();
                if (interactible != null)
                {
                    interactible.Interact();
                }
                else if (hit.collider.transform.parent != null)
                {
                    interactible = hit.collider.GetComponentInParent<Iinteractible>();
                    if (interactible != null)
                    {
                        interactible.Interact();
                    }
                }
            }
        }
    }
}
