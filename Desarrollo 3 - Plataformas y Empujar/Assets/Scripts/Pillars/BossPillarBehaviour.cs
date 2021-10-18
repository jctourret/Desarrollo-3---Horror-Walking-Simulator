using System.Collections;
using System.Collections.Generic;
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

    [Header("Diegetic Lights")]
    public float waitTime = 60f;
    [SerializeField] List<GameObject> nightLights = new List<GameObject>();

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
        pillarState = PillarState.MoveUp;

        foreach (var light in nightLights)
        {
            light.GetComponent<NightLight_Behaviour>().SetIntensityOfLight(false, waitTime / 2);
        }
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
        OnPillarUp?.Invoke();
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(delayTime);

        timer = waitTime;

        pillarState = PillarState.waiting;
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if (callTheExit == false)
            {
                callTheExit = true;

                var go = Instantiate(exitStairs, position_ExitStairs.position, Quaternion.Euler(Vector3.up));
            }
            else
            {
                pillarState = PillarState.MoveDown;
            }
        }

        foreach (var light in nightLights)
        {
            light.GetComponent<NightLight_Behaviour>().SetIntensityOfLight(true, timer);
        }
    }
}
