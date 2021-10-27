using System.Collections;
using UnityEngine;

public class InitialPillarBehaviour : Pillar
{
    [Header("Player Parent")]
    [SerializeField] private Transform playerParent;
    [SerializeField] private Transform player;

    //===============================================

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

    public void MovePlayerParent()
    {
        player.parent = playerParent;

        player.GetComponent<PlayerMovement>().ActivatePlayer();
    }
}
