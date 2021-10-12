using UnityEngine;
[System.Serializable]
public class UpgradableType
{
    [Header("Values")]
    [SerializeField] string name = "Upgradable";
    [SerializeField] float baseValue;
    [SerializeField] float upgradeEfficiency;
    [SerializeField] int maxUpgradeLevel;
    public float GetBaseValue => baseValue;
    public int MaxLevel => maxUpgradeLevel;
    public float GetUpgradedValue(int level) => baseValue + upgradeEfficiency * Mathf.Clamp(level,0,maxUpgradeLevel); 
    public string GetName => name;


    [Header("Pricing")]
    [SerializeField] int startingPrice;
    [SerializeField] int priceIncrease;

    public int GetStartingPrice => startingPrice;
    public int GetIncreasingPrice => priceIncrease;

    public int GetPrice(int level) => GetStartingPrice + GetIncreasingPrice * Mathf.Clamp(level,0,maxUpgradeLevel); 
    // Constructors
    public UpgradableType() => baseValue = 1;
    public UpgradableType(float newBase) => baseValue = newBase;
}