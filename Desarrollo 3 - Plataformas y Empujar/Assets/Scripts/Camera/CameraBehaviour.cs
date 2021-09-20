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

    [Header("Screen Shake")]
    [SerializeField]
    float duration;
    [SerializeField]
    float magnitude;
    [SerializeField]
    bool cameraShaking;

    private void Awake()
    {
        cameraOfsett = commonOfsett;
        cameraBossOfsett = bossOfsett;
    }

    private void Start()
    {
        OnSendCamera?.Invoke(GetComponent<Camera>());
    }

    private void OnEnable()
    {
        PlayerStats.OnPlayerDamaged += StartCameraShake;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDamaged -= StartCameraShake;
    }


    void StartCameraShake(int i)
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

            transform.localPosition = originalPos + new Vector3(x, y, 0.0f);
            yield return null;
        }
        cameraShaking = false;
        transform.localPosition = originalPos;
    }
}
