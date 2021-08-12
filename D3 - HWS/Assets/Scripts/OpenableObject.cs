using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableObject : AnimatedObject, Iinteractible
{
    protected enum State { Open, Close, Idle };

    void Start()
    {
        animator = GetComponent<Animator>();
        if(animator == null && transform.parent != null)
        {
            animator = GetComponentInParent<Animator>();
        }
        currentState = State.Idle.ToString();
    }
    public void Interact()
    {
        if (currentState == State.Idle.ToString())
        {
            if (previousState == State.Open.ToString())
            {
                changeState(State.Close.ToString());
            }
            else if (previousState == State.Close.ToString() || previousState == null)
            {
                changeState(State.Open.ToString());
            }
            StartCoroutine(SetIdle(State.Idle.ToString(), animator.GetCurrentAnimatorStateInfo(0).length));
        }
    }

}
