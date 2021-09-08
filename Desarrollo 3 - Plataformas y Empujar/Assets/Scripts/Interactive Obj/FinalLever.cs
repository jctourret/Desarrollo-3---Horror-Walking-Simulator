using UnityEngine;
using System;

class FinalLever : InteractiveObject
{
//    //public static Action ActivateObject;

//    public enum StateObject
//    {
//        Available,
//        Open
//    };

//    public GameObject UIposter;
//    public StateObject stateObject;

    [Header("Lever")]
    public Animator stair;

//    Animator animator;
//    bool inRange = false;

//    //========================================

//    private void Start()
//    {
//        UIposter.SetActive(false);

//        animator = GetComponent<Animator>();
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (stateObject == StateObject.Available)
//        {
//            UIposter.SetActive(true);

//            inRange = true;
//        }
//    }

//    private void Update()
//    {
//        if (stateObject == StateObject.Available && inRange == true)
//        {
//            if (Input.GetKeyDown(KeyCode.E))
//            {
//                stateObject = StateObject.Open;
//                UIposter.SetActive(false);
//                animator.SetTrigger("Open");
//            }
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (stateObject == StateObject.Available)
//        {
//            UIposter.SetActive(false);

//            inRange = false;
//        }
//    }

    //========================================
    // Usado en la animacion de la Lever

    public void ActivateLever()
    {
        //ActivateObject.Invoke();

        stair.SetTrigger("Up");
    }

}
