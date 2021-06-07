using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreCounter : MonoBehaviour
{
    
    public static int score;
    public int scoreAmount;
    public Text scoretext;


    void Start()
    {
        scoretext.text = $"Score: 0 pts";
        score = scoreAmount;
        scoretext = GetComponent<Text>();
        IncreaseScore(0);
    }

    public void IncreaseScore(int value)
    {
        score += value;
        scoreAmount = score;
        scoretext.text = String.Format("Score: {0} pts", score);
    }
}
