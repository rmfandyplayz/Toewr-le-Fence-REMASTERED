using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CustomUnityEvent;

public class GoldManager : MonoBehaviour
{
    [SerializeField] private int gold;
    public TextMeshProUGUI moneyText;
    public static GoldManager instance;
    public UEventInt OnGoldAdded;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        AddGold(0);
    }

    public void AddGold(int goldGiven)
    {
        gold += goldGiven;
        moneyText.text = $"{gold}";
        OnGoldAdded?.Invoke(gold);
    }

    public static int GetGold()
    {
        if(instance != null)
        {
            return instance.gold;
        }
        else
        {
            return -1;
        }
    }

}
