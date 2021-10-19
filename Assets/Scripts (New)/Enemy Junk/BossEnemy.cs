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
        for(int i = 0; i < enemyInfo.maxEnemies2Spawn; i++)
        {
            var enemy = SpawningManagement.SpawnEnemy(waveObject, spawner.enemyPrefab, spawner.path);
            enemy.transform.position = this.transform.position;
            yield return new WaitForSeconds(enemyInfo.enemySpawnDelay);
        }
    }




}
