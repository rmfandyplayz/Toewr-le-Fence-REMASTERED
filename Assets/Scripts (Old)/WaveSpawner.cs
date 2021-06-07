using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    

    public List <GameObject> enemyPrefab;
    public Transform spawnPoint;
    public Text wavenumbertext;
    public Button startwavebutton;
    //public float timeBetweenWaves = 1f;
    //public float countdown = 1f;
    public int waveNumber = 0;
    public float roundHealth = 5;
    public float roundShields = 5;
    public float rhAdder = 5;
    public float rsAdder = 5;
    public float timebetweenEnemySpawns;
    public static uint aliveEnemies = 0;
    public money moneycontrol;
    [Header("EnemyFireDamageStuff")]
    public GameObject enemyfirescript;
    public GameObject fastenemyfirescript;
    public GameObject obsidianenemyfirescript;
    public GameObject shieldedenemyfirescript;
    public GameObject shieldedfastenemyfirescript;
    public GameObject shieldedobsidianenemyfirescript;
    public GameObject knucklesenemyfirescript;

    void Start()
    {
        moneycontrol.isMoneyActive = false;
        wavenumbertext.text = $"Wave {waveNumber}";
        aliveEnemies = 0;
        StartCoroutine(DelayedUpdateUI(0.1f));
    }
    public static void AddEnemes(uint numberofEnemies)
    {
        aliveEnemies += numberofEnemies;
    }

    public static void RemoveEnemies(uint numberofEnemies)
    {
        if (aliveEnemies > 0) {
            aliveEnemies -= numberofEnemies;
        }

    }

    public void StartWaveButtonClick()
    {
        startwavebutton.interactable = false;
        StartCoroutine(SpawnWave());
    }

    void Update()
    { 
        if (aliveEnemies == 0 && startwavebutton.interactable == false)
        {
            startwavebutton.interactable = true;
            moneycontrol.isMoneyActive = false;
        }
    }

    /*
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }
    */
    IEnumerator DelayedUpdateUI(float timer)
    {
        yield return new WaitForSeconds(timer);
        wavenumbertext.text = $"Wave {waveNumber}";
    }


    IEnumerator SpawnWave()
    {
        moneycontrol.isMoneyActive = true;
        waveNumber+= 1;
        AddEnemes((uint)waveNumber);

        enemyfirescript.GetComponent<Enemy>().firedamageamount += 0.005f;
        fastenemyfirescript.GetComponent<Enemy>().firedamageamount += 0.005f;
        obsidianenemyfirescript.GetComponent<Enemy>().firedamageamount += 0.005f;
        shieldedenemyfirescript.GetComponent<Enemy>().firedamageamount += 0.005f;
        shieldedfastenemyfirescript.GetComponent<Enemy>().firedamageamount += 0.005f;
        shieldedobsidianenemyfirescript.GetComponent<Enemy>().firedamageamount += 0.005f;
        knucklesenemyfirescript.GetComponent<Enemy>().firedamageamount += 0.005f;


        roundHealth += rhAdder;
        roundShields += rsAdder;
        wavenumbertext.text = $"Wave {waveNumber}";
        for (int i = 0; i < waveNumber; i++)
        {

            SpawnEnemy(waveNumber);
            
            yield return new WaitForSeconds(timebetweenEnemySpawns);
            
        }
        



    }

    void SpawnEnemy(int wave)
    {
        
        if (wave <= 5)
        {
            GameObject clone = Instantiate(enemyPrefab[Random.Range(0, 1)], spawnPoint.position, spawnPoint.rotation);
            clone.GetComponent<Enemy>().overallHealth += roundHealth;
            clone.GetComponent<Enemy>().overallShields += roundShields;

        }
        else if (wave <= 10)
        {
            GameObject clone = Instantiate(enemyPrefab[Random.Range(0, 2)], spawnPoint.position, spawnPoint.rotation);
            clone.GetComponent<Enemy>().overallHealth += roundHealth;
            clone.GetComponent<Enemy>().overallShields += roundShields;
        }
        else if (wave <= 18)
        {
            GameObject clone = Instantiate(enemyPrefab[Random.Range(0, 3)], spawnPoint.position, spawnPoint.rotation);
            clone.GetComponent<Enemy>().overallHealth += roundHealth;
            clone.GetComponent<Enemy>().overallShields += roundShields;
        }
        else if (wave <= 29)
        {
            GameObject clone = Instantiate(enemyPrefab[Random.Range(0, 4)], spawnPoint.position, spawnPoint.rotation);
            clone.GetComponent<Enemy>().overallHealth += roundHealth;
            clone.GetComponent<Enemy>().overallShields += roundShields;
        }
        else if (wave <= 35)
        {
            GameObject clone = Instantiate(enemyPrefab[Random.Range(0, 5)], spawnPoint.position, spawnPoint.rotation);
            clone.GetComponent<Enemy>().overallHealth += roundHealth;
            clone.GetComponent<Enemy>().overallShields += roundShields;
        }
        else if (wave <= 40)
        {
            GameObject clone = Instantiate(enemyPrefab[Random.Range(0, 6)], spawnPoint.position, spawnPoint.rotation);
            clone.GetComponent<Enemy>().overallHealth += roundHealth;
            clone.GetComponent<Enemy>().overallShields += roundShields;
        }
        else
        {
            GameObject clone = Instantiate(enemyPrefab[Random.Range(0, 7)], spawnPoint.position, spawnPoint.rotation);
            clone.GetComponent<Enemy>().overallHealth += roundHealth;
            clone.GetComponent<Enemy>().overallShields += roundShields;
        }
        
    }

}
