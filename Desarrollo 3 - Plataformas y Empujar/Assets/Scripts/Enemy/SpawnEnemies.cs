using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    List<GameObject> enemiesToSpawn;
    List<Vector3> spawnPoints;
    private void Start()
    {
         
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
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            Vector3 enemyPosition = spawnPoints[i];
            GameObject go = Instantiate(enemiesToSpawn[i],enemyPosition,Quaternion.identity);
            go.GetComponent<EnemyAI>().camera = EnemyManager.instance.camera;
        }
    }
}

