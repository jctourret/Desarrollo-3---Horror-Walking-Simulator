using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPillarBehaviour : MonoBehaviour
{
    //===============================================

    [Header("Delay Animation")]
    [Range(0, 2)]
    public float delayTime = 1.5f;

    [Header("Diegetic Lights")]
    public float destroyTime = 10f;
    [SerializeField] List<GameObject> nightLights = new List<GameObject>();

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

    private void Start()
    {
        animator = GetComponent<Animator>();
        pillarState = PillarState.MoveUp;

        foreach (var light in nightLights)
        {
            light.GetComponent<NightLight_Behaviour>().SetIntensityOfLight(false, destroyTime);
        }
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

        foreach (var light in nightLights)
        {
            light.GetComponent<NightLight_Behaviour>().SetIntensityOfLight(true, timer);
        }
    }

    IEnumerator MoveDownPillar()
    {
        animator.SetTrigger("Change");

        yield return new WaitForSeconds(delayTime);

        Destroy(this.gameObject);
    }

}
