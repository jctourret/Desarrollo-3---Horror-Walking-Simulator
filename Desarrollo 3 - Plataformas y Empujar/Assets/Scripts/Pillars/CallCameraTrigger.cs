using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CallCameraTrigger : MonoBehaviour
{

    [Range(0, 2)]
    public float smoothSpeed = 0.125f;

    public bool isACommonPillar = true;
    Vector3 offset;

    public Camera cam;

    //=============================================

    private void OnEnable()
    {
        CameraBehaviour.OnSendCamera += GetCamera;
    }

    private void OnDisable()
    {
        CameraBehaviour.OnSendCamera -= GetCamera;
    }

    private void Start()
    {
        if (isACommonPillar)
        {
            offset = CameraBehaviour.cameraOfsett;
        }
        else
        {
            offset = CameraBehaviour.cameraBossOfsett;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            StartCoroutine(MoveCamera());
        }
    }

    //=============================================
        
    void GetCamera(Camera newCamera)
    {
        cam = newCamera;
    }
    
    IEnumerator MoveCamera()
    {
        float time = 0f;
        
        Vector3 desiredPosition = this.transform.position + offset;

        while (time <= 1)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, desiredPosition, time * smoothSpeed);

            time += Time.deltaTime;
            
            yield return null;
        }

        this.gameObject.SetActive(false);
    }
}
