using UnityEngine;

public class SpawnEndlessEnemies : SpawnEnemies
{
    [Header("Boss Enemy")]
    [SerializeField] private GameObject boss;
    [SerializeField] private Transform bossTransform;

    [Header("Enemies")]
    [SerializeField] private GameObject[] allEnemies;

    [Header("Spawn Variables")]
    [SerializeField] private float spawnInterval = 2;
    [SerializeField] private float maxEnemies = 4;
    
    private float spawnTimer = 0;
    private bool startFight = false;

    private void OnEnable()
    {
        Boss_StartLever.ActivateFight += StartFight;
        BossStats.ActivateDeath += FinishFight;
    }

    private void OnDisable()
    {
        Boss_StartLever.ActivateFight -= StartFight;
        BossStats.ActivateDeath -= FinishFight;
    }

    void Update()
    {
        if(startFight)
        {
            if(spawnInterval > spawnTimer)
            {
                spawnTimer += Time.deltaTime;
            }
            else
            {
                for (int i = 0; i < enemiesSpawned.Count; i++)
                {
                    if(enemiesSpawned[i] == null)
                        enemiesSpawned.RemoveAt(i);
                }

                spawnTimer = 0;
                if (enemiesSpawned.Count < maxEnemies)
                {
                    SpawnByOne();
                }
            }
        }
    }

    public void SpawnByOne()
    {
        int spawn = Random.Range(0, toSpawn.Count);
        int enemy = Random.Range(0, allEnemies.Length);

        Vector3 enemyPosition = toSpawn[spawn].transform.position;
        GameObject go = Instantiate(allEnemies[enemy], enemyPosition, Quaternion.identity);
        enemiesSpawned.Add(go.GetComponent<EnemyAI>());
        go.GetComponent<EnemyAI>().target = EnemyManager.instance.player;
        go.GetComponent<EnemyAI>().cam = EnemyManager.instance.cam;
    }

    public void StartFight()
    {
        startFight = true;
        SpawnBoss();
    }

    public void FinishFight()
    {
        startFight = false;
    }

    void SpawnBoss()
    {
        Vector3 enemyPosition = bossTransform.transform.position;
        GameObject go = Instantiate(boss, enemyPosition, Quaternion.identity);
        go.GetComponent<EnemyAI>().target = EnemyManager.instance.player;
        go.GetComponent<EnemyAI>().cam = EnemyManager.instance.cam;
    }
}
