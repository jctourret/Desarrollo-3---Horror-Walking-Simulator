using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CallCameraTrigger : MonoBehaviour
{

    private void OnEnable()
    {
        CameraBehaviour.OnSendCamera += GetCamera;
    }

    private void OnDisable()
    {
        CameraBehaviour.OnSendCamera -= GetCamera;
    }

    public float lerpTime = 1.5f;

    [Range(0, 2)]
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public new Camera camera;

    void GetCamera(Camera newCamera)
    {
        camera = newCamera;
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
            Vector3 smoothedPosition = Vector3.LerpUnclamped(camera.transform.position, desiredPosition, smoothSpeed);
            camera.transform.position = smoothedPosition;

            yield return null;
        }

        this.gameObject.SetActive(false);
    }

}
