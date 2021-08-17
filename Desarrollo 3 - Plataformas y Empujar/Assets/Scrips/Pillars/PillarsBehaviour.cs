using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PillarsBehaviour : MonoBehaviour
{
    public static Action IsCollapsing;
    public static Action CreatePillar;

    //===============================================
    
    [Header("Delay Animation")]
    public Animator animator;
    [Range(0,2)]
    public float delayTime = 1f;

    [Header("UI Timer")]
    public GameObject timerText;
    public float waitTime = 30f;
    public float destroyTime = 5f;

    //===============================================

    enum PillarState
    {
        MoveUp,
        waiting,
        MoveDown
    };
    PillarState pillarState;

    float timer = 0f;
    bool timerToDestroy = false;


    //===============================================

    private void Start()
    {
        timerText.SetActive(false);

        CreatePillar();
    }

    private void OnEnable()
    {
        pillarState = PillarState.MoveUp;

        timer = waitTime;
    }

    private void Update()
    {
        switch (pillarState)
        {
            case PillarState.MoveUp:
                StartCoroutine(MoveUpPillar());
                break;
            
            case PillarState.waiting:
                UpdateTimer();
                break;
            
            case PillarState.MoveDown:
                StartCoroutine(MoveDownPillar());
                break;
        }
    }

    IEnumerator MoveUpPillar()
    {
        yield return new WaitForSeconds(delayTime);

        timerText.SetActive(true);

        pillarState = PillarState.waiting;
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if(timerToDestroy == false)
            {
                timer = destroyTime;
                timerToDestroy = true;

                IsCollapsing();

                timerText.GetComponent<TextMeshPro>().color = Color.red;
            }
            else
            {
                pillarState = PillarState.MoveDown;
            }
        }

        timerText.GetComponent<TextMeshPro>().text = timer.ToString("0");
    }

    IEnumerator MoveDownPillar()
    {
        animator.SetTrigger("Change");

        timerText.SetActive(false);

        yield return new WaitForSeconds(delayTime);

        Destroy(this.gameObject);
    }

}