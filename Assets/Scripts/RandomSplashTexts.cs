using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSplashTexts : MonoBehaviour
{
    public List<string> splashtexts = new List<string> {
        "test", "test 2", "test 3", "test 4"
    };


    void Start()
    {
        GetComponent<Text>().text = RandomMessage();

    }

    private string RandomMessage()
    {
        int randomnumber = Random.Range(0, splashtexts.Count);
        return splashtexts[randomnumber];

    }


}
