using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class BossPillarBehaviour : MonoBehaviour
{
    public static Action UIplayerToken;

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

    private void Awake()
    {
        timerText.SetActive(false);

        pillarState = PillarState.MoveUp;
    }

    private void OnEnable()
    {
        Boss_StartLever.ActivateObject += StartCollapse;
    }

    private void OnDisable()
    {
        Boss_StartLever.ActivateObject -= StartCollapse;        
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            this.transform.GetComponent<BoxCollider>().enabled = false;

            UIplayerToken.Invoke();
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
        timer = waitTime;

        pillarState = PillarState.waiting;
    }

    //IEnumerator MoveUpPillar()
    //{
    //    yield return new WaitForSeconds(delayTime);
    //
    //    timerText.SetActive(true);
    //
    //    pillarState = PillarState.waiting;
    //}

    void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if (callTheExit == false)
            {
                callTheExit = true;

                timerText.SetActive(false);

                var go = Instantiate(exitStairs, position_ExitStairs.position, Quaternion.Euler(Vector3.up));
            }
            else
            {
                pillarState = PillarState.MoveDown;
            }
        }

        timerText.GetComponent<TextMeshPro>().text = timer.ToString("0");
    }
}
