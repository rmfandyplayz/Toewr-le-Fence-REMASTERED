using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretConfiguration : MonoBehaviour
{
    public TurretSettings tsettings;
    public List<GameObject> targets;
    public GameObject bulletPrefab;
    public SerializedDictionary<TypeOfUpgrade, UpgradeCounterInfo> upgradesCounter;
    public List<Transform> firePointList = new List<Transform>();
    private GameObject currentTarget;
    private bool isOnCooldown = false;
    public TurretSpriteHandler spriteHolder; 
    public TurretRangeHandler rangeHolder; 
    private TurretAttackType attackType;

    public void Initialize(bool showTurretRange = false)
    {
        name = tsettings.turretName;
        spriteHolder.Initialize(tsettings.turretSprite, tsettings.colliderPositionAndSize);
        rangeHolder.Initialize(tsettings.range.GetBaseValue, tsettings.rangeVisualizationPosition);
        rangeHolder.ToggleRangeVisual(showTurretRange);
        
        if(tsettings.turretType!= null)
        {
            attackType = gameObject.AddComponent(tsettings.turretType.Type) as TurretAttackType;
            attackType.InitializeScript(this);
        }

        InitUpgrade();      
    }

    public void AddTarget(GameObject possibleTarget)
    {
        attackType.AddTargetToList(possibleTarget);
    }

    public void RemoveTarget(GameObject possibleTarget)
    {
        attackType.RemoveTargetFromList(possibleTarget);
    }

    public void UpdateTarget()
    {
        attackType.UpdateTargetList();
    }

    private void Update()
    {
        if(tsettings.canRotate == true)
        {
            attackType.Aim();
        }
        attackType.PerformAttack(tsettings.fireRate.GetUpgradedValue(CounterValue(TypeOfUpgrade.FireRate)));
    }

    private void RotateTurret()
    {
        Vector3 dir = currentTarget.transform.position - transform.position;
        float targetAngle = Vector2.SignedAngle(Vector2.up, dir);
        // Debug.LogWarning(Vector2.SignedAngle(Vector2.up, dir));
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles,
                                                Vector3.forward * targetAngle,
                                                tsettings.rotateSpeed.GetUpgradedValue(CounterValue(TypeOfUpgrade.Rotation)));
    }

    public void InitUpgrade()
    {
        foreach(TypeOfUpgrade upgrade in tsettings.upgrades)
        {
            if(upgrade == TypeOfUpgrade.Rotation)
            {
                upgradesCounter.Add(upgrade, new UpgradeCounterInfo(tsettings.rotateSpeed));
            }
            if(upgrade == TypeOfUpgrade.Range)
            {
                upgradesCounter.Add(upgrade, new UpgradeCounterInfo(tsettings.range));
            }
            if(upgrade == TypeOfUpgrade.FireRate)
            {
                upgradesCounter.Add(upgrade, new UpgradeCounterInfo(tsettings.fireRate));
            }
            if(upgrade == TypeOfUpgrade.BulletDamage)
            {
                upgradesCounter.Add(upgrade, new UpgradeCounterInfo(tsettings.bulletSetup.bulletDamage));
            }
            if(upgrade == TypeOfUpgrade.DankChance)
            {
                upgradesCounter.Add(upgrade, new UpgradeCounterInfo(tsettings.bulletSetup.dankDmgChance));
            }
            if (upgrade == TypeOfUpgrade.SurrealChance)
            {
                upgradesCounter.Add(upgrade, new UpgradeCounterInfo(tsettings.bulletSetup.surrealDmgChance));
            }
            if (upgrade == TypeOfUpgrade.NoScopeChance)
            {
                upgradesCounter.Add(upgrade, new UpgradeCounterInfo(tsettings.bulletSetup.NoscopeDmgChance));
            }
            upgradesCounter[upgrade].Name = upgrade.ToString();
        }
    }
    public int CounterValue(TypeOfUpgrade upgradeName) 
    {
        return upgradesCounter.ContainsKey(upgradeName)? upgradesCounter[upgradeName].Counter : 0; 
    }
    
    public void BuyUpgrade(TypeOfUpgrade upgradeKey)
    {
        var price = upgradesCounter[upgradeKey].Upgrade.GetPrice(CounterValue(upgradeKey));
        GoldManager.instance.AddGold(-price);
    }

    public void ApplyUpgrade()
    {
        rangeHolder.UpdateRange(tsettings.range.GetUpgradedValue(CounterValue(TypeOfUpgrade.Range)));
    }


}


[System.Serializable]
public class UpgradeCounterInfo
{
    [SerializeField] string name = "";
    [SerializeField] int counter = 0;
    [SerializeField] UpgradableType upgradable;
    [SerializeField] int maxCounter = 0;
    public string Name {
        get{
            return name;
        }
        set
        {
            name = value;
        }
    }
    public int Counter {
        get{
            return counter;
        }
        set
        {
            counter = value;
        }
    }
    public UpgradableType Upgrade {
        get{
            return upgradable;
        }
        private set
        {
            upgradable = value;
        }
    }

    public UpgradeCounterInfo(UpgradableType upgrade)
    {
        Counter = 0;
        Upgrade = upgrade;
    }
}

public static class SerializedDictionaryExtension
{
    public static int CounterValue(this SerializedDictionary<TypeOfUpgrade, UpgradeCounterInfo> dictionary, TypeOfUpgrade key)
    {
        return dictionary.ContainsKey(key) ? dictionary[key].Counter : 0;
    }

    public static int CounterValue(this SerializedDictionary<TypeOfUpgrade, int> dictionary, TypeOfUpgrade key)
    {
        return dictionary.ContainsKey(key) ? dictionary[key] : 0;
    }
}