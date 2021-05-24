using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Play_Button : MonoBehaviour {


    public void StartGame(float timer)
    {
        StartCoroutine(startinggame(timer));
    }



    IEnumerator startinggame(float timer)
    {
        yield return new WaitForSecondsRealtime(timer);
        SceneManager.LoadScene("Load Game Scene");
    }
}
