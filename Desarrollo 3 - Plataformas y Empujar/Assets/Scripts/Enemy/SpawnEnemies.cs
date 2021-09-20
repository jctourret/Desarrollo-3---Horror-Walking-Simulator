﻿using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    List<EnemyAndPoint> toSpawn;
    public List<EnemyAI> enemiesSpawned;

    private void Start()
    {
        toSpawn = new List<EnemyAndPoint>();
        enemiesSpawned = new List<EnemyAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Spawn();
        }
    }

    void Spawn()
    {        
        for (int i = 0; i < toSpawn.Count; i++)
        {
            Vector3 enemyPosition = toSpawn[i].transform.position;
            GameObject go = Instantiate(toSpawn[i].enemy,enemyPosition,Quaternion.identity);
            enemiesSpawned.Add(go.GetComponent<EnemyAI>());
            go.GetComponent<EnemyAI>().cam = EnemyManager.instance.cam;
        }
    }
}

[System.Serializable]
public class EnemyAndPoint
{
    [SerializeField]
    public GameObject enemy;
    [SerializeField]
    public Transform transform;
}