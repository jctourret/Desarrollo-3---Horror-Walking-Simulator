using System.Collections;
using UnityEngine;

public class ActivateTrap : MonoBehaviour
{
    public Animator anim;

    public float timeToActivate = 1f;
    public float activeTime = 2f;
    public string soundEventName = "Missing Sound";

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

        AkSoundEngine.PostEvent(soundEventName, gameObject);

        anim.SetTrigger("Activate");
        isActive = state;
    }
}
