using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrap : MonoBehaviour
{
    public Animator anim;

    public float timeToActivate = 1f;
    public float activeTime = 2f;

    bool isActive = false;
    float time = 0f;

    //=============================================

    private void Update()
    {
        if (isActive)
        {
            time += Time.deltaTime;

            if (time > activeTime)
            {
                time = 0f;
                StartCoroutine(CallHideTrap(false));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive == false)
        {
            StartCoroutine(CallHideTrap(true));
        }
    }

    IEnumerator CallHideTrap(bool state)
    {
        yield return new WaitForSeconds(timeToActivate);

        anim.SetTrigger("Activate");
        isActive = state;
    }
}
