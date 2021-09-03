using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class InitialPillarBehaviour : MonoBehaviour
{
    //===============================================

    [Header("Delay Animation")]
    [Range(0, 2)]
    public float delayTime = 1.5f;

    [Header("UI Timer")]
    public GameObject timerText;
    public float destroyTime = 10f;

    //===============================================

    enum PillarState
    {
        MoveUp,
        waiting,
        MoveDown
    };
    PillarState pillarState;

    float timer = 0f;

    Animator animator;

    //===============================================

    private void Awake()
    {
        animator = GetComponent<Animator>();
        timerText.SetActive(false);
        pillarState = PillarState.MoveUp;
    }

    private void OnEnable()
    {
        StartLever.ActivateObject += StartCollapse;
    }

    private void OnDisable()
    {
        StartLever.ActivateObject -= StartCollapse;
    }

    private void Update()
    {
        switch (pillarState)
        {
            case PillarState.waiting:
                UpdateTimer();
                break;
        }
    }

    //===============================================

    void StartCollapse()
    {
        StartCoroutine(WaitTime());
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(delayTime);

        timerText.SetActive(true);
        timer = destroyTime;

        pillarState = PillarState.waiting;
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            pillarState = PillarState.MoveDown;
            StartCoroutine(MoveDownPillar());
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
