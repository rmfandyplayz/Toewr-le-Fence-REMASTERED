using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public GameObject pausescreen;
    private bool isPaused = false;
    void Start()
    {
        isPaused = false;
        pausescreen.SetActive(false);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        isPaused = !isPaused;
        pausescreen.SetActive(isPaused);
    }

    public void SaveAndQuit()
    {

    }



}
