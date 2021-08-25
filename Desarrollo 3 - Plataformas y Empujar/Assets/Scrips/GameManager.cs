using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static Func<Camera> RecieveCamera;

    Camera mainCamera;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mainCamera = RecieveCamera?.Invoke();
    }
    
}
