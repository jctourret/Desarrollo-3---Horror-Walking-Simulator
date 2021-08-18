using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    [Range(1, 3)]
    public int maxEnemies;
    int enemiesToSpawn;
    List<Vector3> spawnPoints;

    GameObject enemy;

    private void Start()
    {
        enemy = Resources.Load<GameObject>("Enemy");
        enemiesToSpawn = Random.Range(1, maxEnemies);
        spawnPoints = new List<Vector3>();
        for(int i = 0; i< transform.childCount; i++)
        {
            spawnPoints.Add(transform.GetChild(i).GetComponent<Transform>().position);
        }
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
        
        for (int i = 0; i < enemiesToSpawn; enemiesToSpawn--)
        {
            Vector3 enemyPosition = spawnPoints[Random.Range(0,spawnPoints.Count)];
            GameObject go = Instantiate(enemy,enemyPosition,Quaternion.identity);
        }

    }

}

