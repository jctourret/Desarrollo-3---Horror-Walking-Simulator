using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PillarsBehaviour : MonoBehaviour
{
    public static Action UIplayerToken;

    public static Action OnPillarUp;
    public static Action CreatePillar;

    //===============================================

    [Header("Delay Animation")]
    public Animator animator;
    [Range(0,2)]
    public float delayTime = 1f;

    [Header("UI Timer")]
    public GameObject timerText;
    public float waitTime = 30f;
    public float destroyTime = 5f;

    [Header("Special Room")]
    [SerializeField] bool specialRoom = false;
    [SerializeField] Transform parentRoom;

    GameObject room;

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

    //===============================================

    private void OnEnable()
    {
        GetARoom();
    }

    private void Start()
    {
        timerText.SetActive(false);
        
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            StartCoroutine(StartCollapse());

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

    IEnumerator StartCollapse()
    {
        OnPillarUp?.Invoke();

        yield return new WaitForSeconds(delayTime);

        timer = waitTime;
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

                CreatePillar?.Invoke(); // Crea el nuevo pilar

                timerText.GetComponent<TextMeshPro>().color = Color.red;
            }
            else
            {
                pillarState = PillarState.MoveDown;
                StartCoroutine(MoveDownPillar());
            }
        }

        timerText.GetComponent<TextMeshPro>().text = timer.ToString("0");
    }

    IEnumerator MoveDownPillar()
    {
        animator.SetTrigger("Change");

        /// ATENCION!
        /// Esta parte del codigo esta comentada porque genera errores que detienen el codigo de esta corrutina, haciendo que no continue.
        //for(int i = 0; i < room.GetComponentInChildren<SpawnEnemies>().enemiesSpawned.Count; i++)
        //{
        //    room.GetComponentInChildren<SpawnEnemies>().enemiesSpawned[i].pilarFalls();
        //}

        timerText.SetActive(false);        

        yield return new WaitForSeconds(delayTime);

        Destroy(this.gameObject);
    }

}