using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    enum SpawnSide {Top,Bottom,Left,Right};
    MeshRenderer currentPlatform;
    MeshRenderer enemyRenderer;
    GameObject enemyInstance;
    private void Start()
    {
        enemyInstance = Resources.Load<GameObject>("Enemy");
        Player.OnPlayerLand += changeCurrentPlatform;
    }
    void SpawnEnemy()
    {
        float xCoord = 0;
        float zCoord = 0;
        SpawnSide side = (SpawnSide)Random.Range((float)SpawnSide.Top,(float)SpawnSide.Right);
        switch (side)
        {
            case SpawnSide.Top:
                xCoord = Random.Range(currentPlatform.transform.position.x - currentPlatform.bounds.extents.x, currentPlatform.transform.position.x + currentPlatform.bounds.extents.x);
                zCoord = currentPlatform.transform.position.z  + currentPlatform.bounds.extents.z;
                break;
            case SpawnSide.Bottom:
                xCoord = Random.Range(currentPlatform.transform.position.x - currentPlatform.bounds.extents.x, currentPlatform.transform.position.x + currentPlatform.bounds.extents.x);
                zCoord = currentPlatform.transform.position.z  - currentPlatform.bounds.extents.z;
                break;
            case SpawnSide.Left:
                zCoord = Random.Range(currentPlatform.transform.position.z - currentPlatform.bounds.extents.z, currentPlatform.transform.position.z + currentPlatform.bounds.extents.z);
                xCoord = currentPlatform.transform.position.x  - currentPlatform.bounds.extents.x;
                break;
            case SpawnSide.Right:
                zCoord = Random.Range(currentPlatform.transform.position.z - currentPlatform.bounds.extents.z, currentPlatform.transform.position.z + currentPlatform.bounds.extents.z);
                xCoord = currentPlatform.transform.position.x + currentPlatform.bounds.extents.x;
                break;
        }
        enemyRenderer = enemyInstance.GetComponent<MeshRenderer>();
        GameObject enemy = Instantiate(enemyInstance,new Vector3(xCoord, currentPlatform.transform.position.y + enemyRenderer.bounds.extents.y,zCoord),Quaternion.identity);
    }

    void changeCurrentPlatform(GameObject platform)
    {
        if(currentPlatform != platform.GetComponent<MeshRenderer>())
        {
            currentPlatform = platform.GetComponent<MeshRenderer>(); 
        }
    }
}
