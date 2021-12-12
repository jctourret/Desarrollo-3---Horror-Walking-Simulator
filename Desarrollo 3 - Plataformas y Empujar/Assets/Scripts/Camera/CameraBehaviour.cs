using System;
using System.Collections;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Screen Shake")]
    [SerializeField] float duration;
    [SerializeField] float magnitude;
    [SerializeField] bool cameraShaking;

    static Camera cam;

    // ===============================

    private void Awake()
    {
        cam = this.transform.GetComponent<Camera>();
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

    // ===============================

    public static Camera GetCamera()
    {
        return cam;
    }
}
