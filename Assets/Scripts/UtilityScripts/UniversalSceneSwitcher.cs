using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversalSceneSwitcher : MonoBehaviour
{
    public static void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public static void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
