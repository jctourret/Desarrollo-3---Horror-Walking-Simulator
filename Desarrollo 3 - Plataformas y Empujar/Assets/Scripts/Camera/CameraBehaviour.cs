using System;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public static Action<Camera> OnSendCamera;

    [SerializeField] Vector3 commonOfsett;
    [SerializeField] Vector3 bossOfsett;

    public static Vector3 cameraOfsett;
    public static Vector3 cameraBossOfsett;

    private void Awake()
    {
        OnSendCamera?.Invoke(GetComponent<Camera>());

        cameraOfsett = commonOfsett;
        cameraBossOfsett = bossOfsett;
    }
}
