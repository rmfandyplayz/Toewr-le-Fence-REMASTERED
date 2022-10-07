using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [SerializeField] private WaveObjects waveObject;
    [SerializeField] private EnemySetup enemyInfo;
    [SerializeField] private SpawningManagement spawner;

    public void InitializeBossEnemy(WaveObjects waveInfo, EnemySetup enemyInformation, SpawningManagement enemySpawner)
    {
        waveObject = waveInfo;
        enemyInfo = enemyInformation;
        spawner = enemySpawner;
        StartCoroutine(SpawnBoss());
    }

    public Vector3 SpawnEnemyRandomPos()
    {
        float randomXValue;
        float randomYValue;

        var min = enemyInfo.minimumBoxSpawnRadius;
        var max = enemyInfo.maximumBoxSpawnRadius;

        if(enemyInfo.boxShapedSpawningPattern == true)
        {
            randomXValue = Random.Range(min.x, max.x);
            randomYValue = Random.Range(min.y, max.y);

            return new Vector3(randomXValue, randomYValue, 0) + transform.position;
        }
        return this.transform.position;
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(enemyInfo.firstEnemyOrGroupSpawnDelay);
        PathMovement path = GetComponentInParent<PathMovement>();
        for (int i = 0; i < enemyInfo.maxEnemies2Spawn; i++)
        {
            for(int j = 0; j < enemyInfo.groupSize; j++)
            {
                var pos = SpawnEnemyRandomPos();
                //Debug.Log(GetComponentInParent<PathMovement>().ReturnIndex());
                var enemy = SpawningManagement.SpawnEnemy(waveObject, spawner.enemyPrefab, spawner.path, path.ReturnIndex(), enemyInfo.enemySpawnFreezeDuration, pos);
                enemy.transform.position = pos;
                enemy.GetComponent<AlignEnemy>().Initialization(path, this.transform.position - this.transform.localPosition, enemyInfo.enemySpawnFreezeDuration, enemy.GetComponentInChildren<EnemyController>().escript.speed);
            }
            yield return new WaitForSeconds(enemyInfo.enemySpawnDelay);
        }
    }
}
