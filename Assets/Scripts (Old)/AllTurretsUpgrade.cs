using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class AllTurretsUpgrade : MonoBehaviour
{
    public AllTurrets turretscript;
    public turretbullet bulletscript;
    public Enemy enemyscript;
    public money moneyscript;
    public int numberofbuttonsactive;
    public CircleRange circlerange;
/*
    [Header("Upgrade Information")]
    public float fireRateUpgradeCost = 100;
    public float maxFireRateUpgradeValue = 10;
    public float fireRatePriceIncreasePerUpgrade = 50;
    [Space]
    public float rangeUpgradeCost = 200;
    public float maxRangeUpgradeValue = 100;
    public float rangeUpgradePriceIncreasePerUpgrade = 100;
    [Space]
    public float damageBuffUpgradeCost = 100;
    public float maxDamageBuffUpgradeValue = 10;
    public float damageBuffPriceIncreasePerUpgrade = 150;
    [Space]
    */
    [Header("Upgrades List")]
    public List <UpgradeType> upgrades;
    public List<int> saveCounter;


    


    // Start is called before the first frame update
    void Start()
    {
        turretscript = this.GetComponent<AllTurrets>();
        bulletscript = turretscript.bulletobject;
        moneyscript = GameObject.FindGameObjectWithTag("MoneyScriptTag").GetComponent<money>();
        circlerange = GetComponent<CircleRange>();
        if (saveCounter.Count == 0)
        {
            UpdateCounter();
        }
        else
        {
            for (int i = 0; i < saveCounter.Count; i++)
            {
                upgrades[i].Counter = saveCounter[i];
            }
        }
    }

    private void UpdateCounter()
    {
        saveCounter.Clear();
        foreach(var u in upgrades)
        {
            saveCounter.Add(u.Counter);
        }
    }

    public void ButtonInfo(List<GameObject> Buttons)
    {
        //saveCounter.Clear();
        circlerange.mainRangeMultiplier = turretscript.range;
        for (int i = 0; i < upgrades.Count; i ++)
        {
            //saveCounter.Add(upgrades[i].Counter);
            if (i >= Buttons.Count)
            {
                break;
            }
            UpgradeType temp = upgrades[i];
            Buttons[i].SetActive(true);
            //Buttons[i].GetComponent<Button>().onClick.AddListener(() => Debug.LogWarning(temp.Name));
            //Buttons[i].GetComponentInChildren<Text>().text = $"{temp.Name}\n${temp.Cost}";

            Buttons[i].GetComponent<UpgradeButtonScript>().Innit(temp);
            Buttons[i].GetComponent<UpgradeButtonScript>().UpdateCost();
            Buttons[i].GetComponent<Button>().onClick.AddListener(() => UpdateCounter());
            //Buttons[i].GetComponent<Button>().onClick.AddListener(()=> Buttons[i].GetComponent<UpgradeButtonScript>().UpdateCost());
        }
        
    }
    
    /*
    -Remove float cost and replace with float upgradePower



    */

    public void FireRateUpgrade(float newFireRate)
    {
        /*
        if (turretscript.fireRate >= maxFireRateUpgradeValue)
        {
            turretscript.fireRate -= upgradePower;




            //moneyscript.spendMoney(fireRateUpgradeCost);
            //fireRateUpgradeCost += fireRatePriceIncreasePerUpgrade;
        }
        */

        turretscript.fireRate = newFireRate;







    }

    public void RangeUpgrade(float newRange)
    {
        /*
        if (turretscript.range <= maxRangeUpgradeValue)
        {
            turretscript.range += upgradePower;
            moneyscript.spendMoney(rangeUpgradeCost);
            rangeUpgradeCost += rangeUpgradePriceIncreasePerUpgrade;
            circlerange.mainRangeMultiplier = turretscript.range;
            circlerange.UpdateRange();
        }
        */
        turretscript.range = newRange;
        circlerange.mainRangeMultiplier = turretscript.range;
        circlerange.UpdateRange();

    }

    public void DamageUpgrade(float newDamage)
    {

        /*
        bulletscript.bulletDamage += upgradePower;
        moneyscript.spendMoney(damageBuffUpgradeCost);
        damageBuffUpgradeCost += damageBuffPriceIncreasePerUpgrade;
        */

        bulletscript.bulletDamage = newDamage;
        
    }

    public void ExplosionRadiusUpgrade(float newExplosionRadius)
    {
        bulletscript.explosionRadius = newExplosionRadius;

    }

    public void ExplosionDamageUpgrade(float newExplosionDamage)
    {
        bulletscript.explosionDamage_subtractfrombulletdamage = newExplosionDamage;
        
    }

    public void FreezeEfficiencyUpgrade(float newFreezeEfficiency)
    {
        bulletscript.FreezeTowerSlowdown = newFreezeEfficiency;
    }

    public void FreezeDurationUpgrade(float newFreezeDuration)
    {
        bulletscript.freezeDuration = newFreezeDuration;

    }

    public void FlamethrowerFireDamageUpgrade(float newFireDamage)
    {
        turretscript.fireDamage = newFireDamage;
    }

    public void TNTYeeterTNTFuse(float newTNTFuse)
    {
        bulletscript.realTNTFuse = newTNTFuse;
    }

    
    

    /*
    public static void Main()
    {
        var functioncall = new AllTurretsUpgrade();
        functioncall.DamageUpgrade(float cost);
    }
    */

}
[System.Serializable]
public class UpgradeEvent:UnityEvent<float>
{
    
}



[System.Serializable]
public class UpgradeType
{
    public string Name;
    public float Cost;
    public float costIncreaseAmount;
    public float startingAmount;
    public float upgradeEfficiency;
    public float maxUpgradeValue;
    public UpgradeEvent UpgradeEvent;
    public bool isMaximum = true;

    public int Counter = 0;

    public void ClickedButton(float newValue)
    {
        UpgradeEvent?.Invoke(newValue);
    }

















}
