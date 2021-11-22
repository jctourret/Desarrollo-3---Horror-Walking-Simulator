using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEndlessEnemies : SpawnEnemies
{
    [SerializeField] float spawnInterval = 2;
    [SerializeField] float maxEnemies = 4;
    float spawnTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if(spawnInterval > spawnTimer)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            spawnTimer = 0;
            if (enemiesSpawned.Count < maxEnemies)
            {
                SpawnByOne();
            }
        }
    }

    public void SpawnByOne()
    {
        for (int i = 0; i < toSpawn.Count; i++)
        {
            Vector3 enemyPosition = toSpawn[i].transform.position;
            GameObject go = Instantiate(toSpawn[i].enemy, enemyPosition, Quaternion.identity);
            enemiesSpawned.Add(go.GetComponent<EnemyAI>());
            go.GetComponent<EnemyAI>().target = EnemyManager.instance.player;
            go.GetComponent<EnemyAI>().cam = EnemyManager.instance.cam;
            i++;
            return;
        }
    }

}
