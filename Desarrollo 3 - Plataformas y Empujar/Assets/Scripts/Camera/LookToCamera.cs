using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    new Camera camera;

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
        transform.LookAt(camera.transform);
    }

    void GetCamera(Camera newCamera)
    {
        camera = newCamera;
    }

}
