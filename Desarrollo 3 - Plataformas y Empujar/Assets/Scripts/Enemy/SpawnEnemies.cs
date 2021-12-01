using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    protected List<EnemyAndPoint> toSpawn;
    public List<EnemyAI> enemiesSpawned;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Spawn();
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void Spawn()
    {        
        for (int i = 0; i < toSpawn.Count; i++)
        {
            Vector3 enemyPosition = toSpawn[i].transform.position;
            GameObject go = Instantiate(toSpawn[i].enemy,enemyPosition,Quaternion.identity);
            enemiesSpawned.Add(go.GetComponent<EnemyAI>());
            go.GetComponent<EnemyAI>().target = EnemyManager.instance.player;
            go.GetComponent<EnemyAI>().cam = EnemyManager.instance.cam;
        }
    }

    public void EnemiesInRoomFall()
    {
        for (int i = 0; i < enemiesSpawned.Count - 1; i++)
        {
            enemiesSpawned[i].Fall();
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