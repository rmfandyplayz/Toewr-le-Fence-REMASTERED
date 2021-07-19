using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CustomUnityEvent;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private int wave;
    public TextMeshProUGUI waveText;
    public static WaveManager instance;
    public UEventInt OnNextWave;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        wave = 0;
    }

    public void NextWave()
    {
        wave++;
        waveText.text = $"Wave: {wave}";
        OnNextWave?.Invoke(wave);
    }

    public static int GetWave()
    {
        if(instance != null)
        {
            return instance.wave;
        }
        else
        {
            return -1;
        }
    }
}
