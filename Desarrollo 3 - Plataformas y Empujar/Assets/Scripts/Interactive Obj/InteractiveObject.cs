using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

abstract class InteractiveObject : MonoBehaviour
{
    //public static Action ActivateObject;

    public enum StateObject
    {
        Closed,
        Available,
        Open
    };

    [Header("Interactive Obj")]
    public StateObject stateObject;
    public GameObject UIposter;

    //========================================

    Animator animator;
    bool inRange = false;

    //========================================

    private void Start()
    {
        UIposter.SetActive(false);

        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stateObject == StateObject.Available)
        {
            UIposter.SetActive(true);

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
                UIposter.SetActive(false);
                animator.SetTrigger("Open");

                //ActivateObject.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (stateObject == StateObject.Available)
        {
            UIposter.SetActive(false);

            inRange = false;
        }
    }
}
