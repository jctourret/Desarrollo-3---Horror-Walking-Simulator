using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObject : MonoBehaviour
{
    
    float timer = 0.0f;
    
    protected string previousState;
    protected string currentState;
    protected Animator animator;
    protected void changeState(string newState)
    {
        if (currentState == newState) { return; }


        animator.Play(newState);
        previousState = currentState;
        currentState = newState;
    }


    protected IEnumerator SetIdle(string IdleState,float animTime)
    {
        while(timer < animTime)
        {
            timer += Time.deltaTime;

            yield return null;
        }
        changeState(IdleState);
        timer = 0.0f;

    }
}
