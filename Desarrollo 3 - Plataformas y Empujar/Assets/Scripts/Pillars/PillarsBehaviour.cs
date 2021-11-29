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

        AkSoundEngine.PostEvent("pilar_aparece", gameObject);

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
            SoundManager.Get().FirstEncounter();

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
            room = go;
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
                if(specialRoom)
                    AkSoundEngine.PostEvent("mono_frase3_despedida", gameObject);
                else
                    AkSoundEngine.PostEvent("pilar_alarma_caida", gameObject);

                timer = destroyTime;
                timerToDestroy = true;

                CreatePillar?.Invoke(); // Crea el nuevo pilar
            }
            else
            {
                pillarState = PillarState.MoveDown;

                if(!specialRoom)
                {
                    room.GetComponentInChildren<SpawnEnemies>().EnemiesInRoomFall();
                }
                StartCoroutine(MoveDownPillar());
            }
        }
        
        foreach (var light in nightLights)
        {
            light.GetComponent<NightLight_Behaviour>().SetIntensityOfLight(timerToDestroy, timer);
        }
    }
}