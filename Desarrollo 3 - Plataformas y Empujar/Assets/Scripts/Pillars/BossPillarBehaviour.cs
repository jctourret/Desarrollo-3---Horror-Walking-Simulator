using UnityEngine;
using System;

public class BossPillarBehaviour : Pillar
{
    public static Action UIplayerToken;
    public static Action CreatePillar;
    public static Action OnPillarUp;

    //===============================================

    [Header("Exit Stairs")]
    [SerializeField] private GameObject exitStairs;
    [SerializeField] private Transform position_ExitStairs;

    bool callTheExit = false; // Se usa para llamar a la salida del "nivel"

    //===============================================

    public override void Start()
    {
        base.Start();

        AkSoundEngine.PostEvent("pilar_final_aparece", gameObject);

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
            AkSoundEngine.PostEvent("partida_musica04_pre_boss", gameObject);

            this.transform.GetComponent<BoxCollider>().enabled = false;

            UIplayerToken.Invoke();
        }
    }

    //===============================================

    public override void StartCollapse()
    {
        AkSoundEngine.PostEvent("partida_musica05_boss", gameObject);

        StartCoroutine(WaitTime());
        OnPillarUp?.Invoke();
    }

    public override void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if (callTheExit == false)
            {
                callTheExit = true;

                var go = Instantiate(exitStairs, position_ExitStairs.position, this.transform.rotation);
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
