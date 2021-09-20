using System;
using System.Collections;
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

    private void OnEnable()
    {
        PlayerStats.OnPlayerDamaged += StartCameraShake;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDamaged -= StartCameraShake;
    }


    void StartCameraShake()
    {
        if (!cameraShaking)
        {
            cameraShaking = true;

            StartCoroutine(CameraShake());
        }
    }

    IEnumerator CameraShake()
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
