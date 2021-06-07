using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowTo : MonoBehaviour {

    public void LoadScreen(float timer)
    {
        StartCoroutine(loadscreen(timer));
    }



    IEnumerator loadscreen(float timer)
    {
        yield return new WaitForSecondsRealtime(timer);
        SceneManager.LoadScene("Game Info");
    }
}
