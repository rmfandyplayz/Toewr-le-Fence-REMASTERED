using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawningManagement : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<WaveObjects> waveObjects;
    public static int aliveEnemies;
    public float chanceOfBossWave;
    [SerializeField] private int currentWaveObject;
    public UnityEvent onAllEnemiesDead;
    private bool isWaveActive = false;
    public Polyline path;

    public static GameObject SpawnEnemy(WaveObjects wave, GameObject enemyPrefab, Polyline path)
    {
        int enemyChoice = Random.Range(0, wave.nonBossEnemies.Count);
        GameObject Enemy = Instantiate(enemyPrefab);
        Enemy.GetComponentInChildren<EnemyController>().escript = wave.nonBossEnemies[enemyChoice];
        var enemyPath = Enemy.GetComponent<PathMovement>();
        enemyPath.path = path;
        enemyPath.speed = Enemy.GetComponentInChildren<EnemyController>().escript.speed;
        return Enemy;
    }

    private void SpawnBoss(WaveObjects information)
    {
        var spawnBoss = SpawnEnemy(information, enemyPrefab, path);
        var controller = spawnBoss.GetComponentInChildren<EnemyController>();
        controller.escript = information.bossEnemies[0];
        spawnBoss.GetComponent<PathMovement>().speed = controller.escript.speed;
        var addComponent = spawnBoss.AddComponent<BossEnemy>();
        addComponent.InitializeBossEnemy(information, controller.escript, this);
    }


    void Start()
    {
        aliveEnemies = 0;
        currentWaveObject = 0;
        WaveManager.instance.OnNextWave.AddListener(Spawn);
    }

    void Update()
    {
        if (isWaveActive == true && aliveEnemies == 0)
        {
            onAllEnemiesDead?.Invoke();
            isWaveActive = false;
        }
    }

    private void Spawn(int waveNumber)
    {
        if (waveObjects != null && waveObjects.Count > 0)
        {
            if (waveObjects[currentWaveObject].endWave < waveNumber && waveObjects.Count > currentWaveObject)
            {
                currentWaveObject++;
            }
            var wave = waveObjects[currentWaveObject];
            float RNG = Random.Range(0.0f, 100.0f);
            if (RNG <= wave.bossWaveChance && wave.bossEnemies.Count > 0)
            {
                SpawnBoss(wave);
            }
            else
            {
                StartCoroutine(PauseBetweenSpawning(0.3f, wave, waveNumber));
            }
            isWaveActive = true;
        }
    }


    IEnumerator PauseBetweenSpawning(float timer, WaveObjects wave, int waveNumber)
    {
        for (int i = 0; i < wave.waveEnemyMultiplier * waveNumber + wave.waveEnemyConstant; i++)
        {
            SpawnEnemy(wave, enemyPrefab, path);
            yield return new WaitForSeconds(timer);
        }
    }
}
