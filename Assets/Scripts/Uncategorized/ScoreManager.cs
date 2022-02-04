using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;
    public TextMeshProUGUI scoreText;
    public static ScoreManager scoreManager;

    private void Awake()
    {
        if (scoreManager == null)
        {
            scoreManager = this;
        }
        AddScore(0);
    }

    public void AddScore(int scoreGiven)
    {
        score += scoreGiven;
        scoreText.text = $"Score: {score}";


    }

    public static int GetScore()
    {
        if (scoreManager != null)
        {
            return scoreManager.score;
        }
        else
        {
            return -1;
        }
    }

}
