using System;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public static Action<Camera> OnSendCamera;
    private void Start()
    {
        OnSendCamera?.Invoke(GetComponent<Camera>());
    }
}
