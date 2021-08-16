using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PillarsBehaviour : MonoBehaviour
{
    [Header("Delay Animation")]
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

    Animator animator;

    //===============================================

    private void Start()
    {
        animator = GetComponent<Animator>();

        timerText.SetActive(false);
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