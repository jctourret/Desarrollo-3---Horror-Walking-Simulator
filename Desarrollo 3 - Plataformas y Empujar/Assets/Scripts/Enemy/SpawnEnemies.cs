﻿using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    List<EnemyAndPoint> toSpawn;
    public List<EnemyAI> enemies;

    private void Start()
    {
        toSpawn = new List<EnemyAndPoint>();
        enemies = new List<EnemyAI>();
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
            enemies.Add(go.GetComponent<EnemyAI>());
            go.GetComponent<EnemyAI>().camera = EnemyManager.instance.camera;
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