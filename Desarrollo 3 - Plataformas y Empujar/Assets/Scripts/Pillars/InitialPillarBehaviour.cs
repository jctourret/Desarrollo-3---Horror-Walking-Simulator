using System.Collections;
using UnityEngine;

public class InitialPillarBehaviour : Pillar
{
    //public override void Awake()
    //{
    //    base.Awake();
    //}

    public override void Start()
    {
        base.Start();
        
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

    public override void UpdateTimer()
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
}
