using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public enum StateObject
    {
        Closed,
        Available,
        Open
    };

    public GameObject posterUI;

    public StateObject stateObject;

    Animator animator;
    bool inRange = false;

    //========================================

    private void Start()
    {
        posterUI.SetActive(false);

        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stateObject == StateObject.Available)
        {
            posterUI.SetActive(true);

            inRange = true;
        }
    }

    private void Update()
    {
        if (stateObject == StateObject.Available && inRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                stateObject = StateObject.Open;
                posterUI.SetActive(false);
                animator.SetTrigger("Open");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (stateObject == StateObject.Available)
        {
            posterUI.SetActive(false);

            inRange = false;
        }
    }
}
