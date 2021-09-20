using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    Camera cam;

    private void OnEnable()
    {
        CameraBehaviour.OnSendCamera += GetCamera;
    }

    private void OnDisable()
    {
        CameraBehaviour.OnSendCamera -= GetCamera;
    }

    private void LateUpdate()
    {
        if (cam != null)
        {
            transform.LookAt(cam.transform);
        }
    }

    void GetCamera(Camera newCamera)
    {
        cam = newCamera;
    }

}
