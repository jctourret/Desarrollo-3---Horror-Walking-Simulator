using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CallCameraTrigger : MonoBehaviour
{
    [Range(0, 2)]
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public SoundManager.MusicState musicType; // Se usa para seleccionar el tipo de musica que va a sonar cuando se entra en collider

    [HideInInspector] public Camera cam;

    //=============================================

    private void Start()
    {
        cam = CameraBehaviour.GetCamera();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            StartCoroutine(MoveCamera());

            SoundManager.Get().ChangeMainMusic(musicType);
        }
    }

    //=============================================
    
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
