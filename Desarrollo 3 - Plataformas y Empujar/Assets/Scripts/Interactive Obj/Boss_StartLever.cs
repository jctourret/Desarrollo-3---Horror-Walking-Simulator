using UnityEngine;
using System;

public class Boss_StartLever : MonoBehaviour
{
    public static Action ActivateObject;

    public enum StateObject
    {
        Available,
        Open
    };

    public GameObject UIposter;

    public StateObject stateObject;

    public SpawnEnemies spawnEnemies;

    Animator animator;
    bool inRange = false;

    //========================================

    private void Start()
    {
        UIposter.SetActive(false);

        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stateObject == StateObject.Available)
        {
            UIposter.SetActive(true);

            inRange = true;
        }
    }

    private void Update()
    {
        if (stateObject == StateObject.Available && inRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                stateObject = StateObject.Open;
                UIposter.SetActive(false);
                animator.SetTrigger("Open");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (stateObject == StateObject.Available)
        {
            UIposter.SetActive(false);

            inRange = false;
        }
    }

    //========================================

    public void ActivateLever()
    {
        ActivateObject?.Invoke();

        spawnEnemies.Spawn();
    }
}
