using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public GameObject player;
    public Camera cam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void assingTarget(GameObject target, GameObject chaser)
    {
        if(chaser.GetComponent<EnemyAI>() != null)
        {
            chaser.GetComponent<EnemyAI>().target = target;
        }
    }

    void GetCamera(Camera newCamera)
    {
        cam = newCamera;
    }
}
