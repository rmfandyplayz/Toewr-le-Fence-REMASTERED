using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SavingOptions : MonoBehaviour
{

    public string sceneToLoad;
    public string towerFolderPath;
    public List<GameObject> towerPrefabSave;
    public List<GameObject> enemyPrefabSave;
    public List<string> savePrefabs;

    private void Awake()
    {
        foreach(var tower in Resources.LoadAll<AllTurrets>(towerFolderPath))
        {
            towerPrefabSave.Add(tower.gameObject);
        }
    }



    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadGame(bool newgame = false)
    {
        SceneManager.LoadScene(sceneToLoad);
        if (!newgame)
        {
            //GetComponent<panelfade>().FadeOut();
            StartCoroutine(LoadingGame());
        }

    }

    private IEnumerator LoadingGame()
    {
        if (PersistentSaveData.Persistent.PersistentData.IsSaved("money settings.json"))
        {
            while (SceneManager.GetActiveScene().name != sceneToLoad)
            {
                yield return new WaitForEndOfFrame();

            }
            Time.timeScale = 0;
            LoadObjects();
            foreach (var test in FindObjectsOfType<PersistentSaveData.Persistent.PersistentReference>())
            {

                test.Load();
            }

            Time.timeScale = 1;
        }
    }

    public void LoadObjects()
    {
        GetComponent<PersistentSaveData.Persistent.PersistentReference>().Load();
        foreach (string name in savePrefabs)
        {
            foreach (GameObject tower in towerPrefabSave)
            {
                if (name.Contains(tower.name))
                {
                    GameObject t = Instantiate(tower);
                    t.GetComponent<CircleRange>().mainRangeMultiplier = t.GetComponent<AllTurrets>().range;
                    t.GetComponent<CircleRange>().ToggleRangeVisual(false, false);
                    t.GetComponent<Collider>().enabled = true;
                    string id = name.Substring(tower.name.Length);
                    foreach (var s in t.GetComponents<PersistentSaveData.Persistent.PersistentReference>())
                    {
                        s.fileName += id;

                        //s.Load();
                    }
                }
            }
        }
    }

    public void SaveGame(bool removeSavedObject = false)
    {
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
        SaveObjects();
        if (WaveSpawner.aliveEnemies != 0)        {            var spawner = FindObjectOfType<WaveSpawner>();            if (spawner != null)            {                spawner.waveNumber -= 1;                spawner.moneycontrol.isMoneyActive = false;            }        }
        //Delete after testing


        foreach (var save in FindObjectsOfType<PersistentSaveData.Persistent.PersistentReference>())
        {
            
            save.Save();
            Debug.LogWarning(save);
        }
        if (removeSavedObject == true)
        {
            Destroy(this.gameObject);
        }
    }

    public void SaveObjects()
    {
        int ID = 1;        foreach (var obj in FindObjectsOfType<AllTurrets>())        {            Debug.Log(obj.ToString());            Debug.Log(obj.name);            savePrefabs.Add(obj.name.Replace("(Clone)", $"_{ID}"));            //j            foreach (var save in obj.GetComponents<PersistentSaveData.Persistent.PersistentReference>())            {
                if (save.fileName.IndexOf("_") == -1)
                {
                    save.fileName += $"_{ID}";
                }
                else
                {
                    save.fileName = save.fileName.Substring(0, save.fileName.IndexOf("_")) + $"_{ID}";
                }                save.Save();            }            ID++;        }

    }


}
