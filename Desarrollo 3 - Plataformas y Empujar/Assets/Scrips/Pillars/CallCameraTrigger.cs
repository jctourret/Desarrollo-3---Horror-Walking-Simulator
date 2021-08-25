using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CallCameraTrigger : MonoBehaviour
{
    public static Func<Camera> RecieveCamera;

    public float lerpTime = 1.5f;

    [Range(0,2)]
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    Camera newCamera;

    private void Start()
    {
        newCamera = RecieveCamera?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            StartCoroutine(MoveCamera());
        }
    }

    IEnumerator MoveCamera()
    {
        float time = 0f;

        while (time < lerpTime)
        {
            time += Time.deltaTime;

            Vector3 desiredPosition = this.transform.position + offset;
            Vector3 smoothedPosition = Vector3.LerpUnclamped(newCamera.transform.position, desiredPosition, smoothSpeed);
            newCamera.transform.position = smoothedPosition;

            yield return null;
        }

        this.gameObject.SetActive(false);
    }

}
