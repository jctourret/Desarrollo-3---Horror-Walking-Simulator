using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BossPillarBehaviour : MonoBehaviour
{
    //public static Action IsCollapsing;
    public static Action CreatePillar;
    public static Action OnPillarUp;
    //===============================================

    [Header("Delay Animation")]
    public Animator animator;
    [Range(0, 2)]
    public float delayTime = 1f;

    [Header("UI Timer")]
    public GameObject timerText;
    public float waitTime = 60f;

    [Header("Exit Stairs")]
    public GameObject exitStairs;
    public Transform position_ExitStairs;

    //===============================================

    enum PillarState
    {
        MoveUp,
        waiting,
        MoveDown
    };
    PillarState pillarState;

    float timer = 0f;
    bool callTheExit = false; // Se usa para llamar a la salida del "nivel"

    //===============================================

    private void Start()
    {
        timerText.SetActive(false);

        CreatePillar?.Invoke();
        StartCoroutine(MoveUpPillar());
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
                break;

            case PillarState.waiting:
                UpdateTimer();
                break;

            case PillarState.MoveDown:
                break;
        }
    }

    IEnumerator MoveUpPillar()
    {
        yield return new WaitForSeconds(delayTime);

        timerText.SetActive(true);

        pillarState = PillarState.waiting;
        OnPillarUp?.Invoke();
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if (callTheExit == false)
            {
                //timer = destroyTime;
                //timerToDestroy = true;
                //
                //IsCollapsing?.Invoke();
                //
                //timerText.GetComponent<TextMeshPro>().color = Color.red;

                callTheExit = true;

                timerText.SetActive(false);

                var go = Instantiate(exitStairs, position_ExitStairs.position, Quaternion.Euler(Vector3.up));
            }
            else
            {
                pillarState = PillarState.MoveDown;
                //StartCoroutine(MoveDownPillar());
            }
        }

        timerText.GetComponent<TextMeshPro>().text = timer.ToString("0");
    }

    IEnumerator MoveDownPillar()
    {
        //animator.SetTrigger("Change");
        //
        //timerText.SetActive(false);
        //
        //yield return new WaitForSeconds(delayTime);
        yield return null;
        //
        //Destroy(this.gameObject);
    }


}
