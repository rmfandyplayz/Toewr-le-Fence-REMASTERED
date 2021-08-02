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
            //temporary
            var wave = waveObjects[currentWaveObject];
            StartCoroutine(PauseBetweenSpawning(0.3f, wave, waveNumber));
            isWaveActive = true;
        }
        else
        {
            Debug.LogError("No Wave Objects Found (From SpawningMangement.cs)");
        }
    }


    IEnumerator PauseBetweenSpawning(float timer, WaveObjects wave, int waveNumber)
    {
        for (int i = 0; i < wave.waveEnemyMultiplier * waveNumber + wave.waveEnemyConstant; i++)
        {
            int enemyChoice = Random.Range(0, wave.nonBossEnemies.Count);
            Debug.LogWarning(enemyChoice + " (SpawningManagement.cs");
            GameObject Enemy = Instantiate(enemyPrefab);
            Enemy.GetComponentInChildren<EnemyController>().escript = wave.nonBossEnemies[enemyChoice];
            Enemy.GetComponent<PathMovement>().path = path;
            yield return new WaitForSeconds(timer);
        }
    }

}
