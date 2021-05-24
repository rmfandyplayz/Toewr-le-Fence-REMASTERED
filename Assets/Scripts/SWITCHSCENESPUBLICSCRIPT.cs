using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SWITCHSCENESPUBLICSCRIPT : MonoBehaviour
{
    public string sceneToSwitch;

    public void ChangeScene(float pause)
    {
        StartCoroutine(SwitchScene(pause));
    }

    IEnumerator SwitchScene(float pause)
    {
        yield return new WaitForSecondsRealtime(pause);
        SceneManager.LoadScene(sceneToSwitch);
    }

    public void SaveGame(bool deleteSO)
    {
        FindObjectOfType<SavingOptions>()?.SaveGame(deleteSO);
        ChangeScene(0.1f);
    }

}
