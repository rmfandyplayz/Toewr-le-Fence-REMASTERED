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



    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(enemyInfo.firstEnemyOrGroupSpawnDelay);

        for(int i = 0; i < enemyInfo.maxEnemies2Spawn; i++)
        {
            for(int j = 0; j < enemyInfo.groupSize; j++)
            {
                var pos = transform.position - transform.localPosition;
                Debug.Log(GetComponentInParent<PathMovement>().ReturnIndex());
                var enemy = SpawningManagement.SpawnEnemy(waveObject, spawner.enemyPrefab, spawner.path, GetComponentInParent<PathMovement>().ReturnIndex(), enemyInfo.enemySpawnDelay, pos);
                enemy.transform.position = pos;
            }
            yield return new WaitForSeconds(enemyInfo.enemySpawnDelay);
        }
        

    }




}
