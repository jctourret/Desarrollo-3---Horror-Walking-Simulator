using System.Collections;
using UnityEngine;
using System;

public class PillarsBehaviour : Pillar
{
    public static Action UIplayerToken;
    public static Action OnPillarUp;
    public static Action CreatePillar;

    //===============================================

    [Header("Special Room")]
    [SerializeField] private bool specialRoom = false;
    [SerializeField] private Transform parentRoom;

    GameObject room;
    bool timerToDestroy = false;

    //===============================================

    private void OnEnable()
    {
        GetARoom();
    }

    public override void Start()
    {
        base.Start();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            StartCoroutine(InitializeCollapse());

            this.transform.GetComponent<BoxCollider>().enabled = false;

            UIplayerToken.Invoke();
        }
    }

    //===============================================

    void GetARoom()
    {
        if (specialRoom == false)
        {
            room = LoaderManager.Get().GetARoom();

            var go = Instantiate(room, parentRoom);
            go.transform.name = room.name;
        }
    }

    IEnumerator InitializeCollapse()
    {
        OnPillarUp?.Invoke();

        timer = waitTime;
        
        pillarState = PillarState.waiting;

        yield return null;
    }

    public override void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            if(timerToDestroy == false)
            {
                timer = destroyTime;
                timerToDestroy = true;

                CreatePillar?.Invoke(); // Crea el nuevo pilar
            }
            else
            {
                pillarState = PillarState.MoveDown;
                StartCoroutine(MoveDownPillar());
            }
        }
        
        foreach (var light in nightLights)
        {
            light.GetComponent<NightLight_Behaviour>().SetIntensityOfLight(timerToDestroy, timer);
        }
    }
}