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

    public void Initialize(bool showTurretRange = false)
    {
        name = tsettings.turretName;
        spriteHolder.Initialize(tsettings.turretSprite, tsettings.colliderPositionAndSize);
        rangeHolder.Initialize(tsettings.range.GetBaseValue, tsettings.rangeVizPosition);
        rangeHolder.ToggleRangeVisual(showTurretRange);
        if(firePointList.Count == 0)
        {
            foreach(var fp in tsettings.firepointPositionRotation)
            {
                var firePoint = new GameObject("FirePoint");
                firePoint.transform.position = fp.position;
            }
        }  
        InitUpgrade();      
    }

    public void AddTarget(GameObject possibleTarget)
    {
        if(possibleTarget.GetComponent<EnemyController>()!= null)
        {
            targets.Add(possibleTarget.gameObject);
            UpdateTarget();
        }
    }

    public void RemoveTarget(GameObject possibleTarget)
    {
        if (possibleTarget.GetComponent<EnemyController>() != null)
        {
            targets.Remove(possibleTarget);
            UpdateTarget();
        }
    }

    public static float FindEnemyDistance(Vector2 towerPosition, Vector2 enemyPos)
    {
        return (towerPosition - enemyPos).magnitude;
    }

    public void UpdateTarget()
    {
        //Debug.Break();
        float closestEnemyDistance = float.MaxValue;
        foreach(GameObject enemy in targets)
        {
            if(enemy == null)
            {
                //targets.Remove(enemy);
                continue;
            }
            float distance = FindEnemyDistance(this.transform.position, enemy.transform.position); //temporary variable
            if (distance < closestEnemyDistance)
            {
                closestEnemyDistance = distance;
                currentTarget = enemy;
            }
        }
        if(targets.Count == 0)
        {
            currentTarget = null;
        }
    }

    private void Update()
    {
        if(currentTarget != null)
        {
            if(tsettings.canRotate)
            {
                RotateTurret();
            }
            if(isOnCooldown == false)
            {
                StartCoroutine(Shoot());
            }
        }
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

    IEnumerator Shoot()
    {
        foreach(Transform firePoint in firePointList)
        {
            GameObject bullet = ObjectPooling.GetGameObject(bulletPrefab);
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            BulletController bc = bullet.GetComponent<BulletController>();
            bc.bscript = tsettings.bullet;
            bc.targetenemy = currentTarget;
            bc.InitUpgrade(upgradesCounter);
            
            isOnCooldown = true;
            yield return new WaitForSeconds(tsettings.fireRate.GetUpgradedValue(CounterValue(TypeOfUpgrade.FireRate)));
            isOnCooldown = false;
        }
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
                upgradesCounter.Add(upgrade, new UpgradeCounterInfo(tsettings.bullet.bulletDamage));
            }
            if(upgrade == TypeOfUpgrade.DankChance)
            {
                upgradesCounter.Add(upgrade, new UpgradeCounterInfo(tsettings.bullet.dankDmgChance));
            }
            if (upgrade == TypeOfUpgrade.SurrealChance)
            {
                upgradesCounter.Add(upgrade, new UpgradeCounterInfo(tsettings.bullet.surrealDmgChance));
            }
            if (upgrade == TypeOfUpgrade.NoScopeChance)
            {
                upgradesCounter.Add(upgrade, new UpgradeCounterInfo(tsettings.bullet.NoscopeDmgChance));
            }
            upgradesCounter[upgrade].Name = upgrade.ToString();
        }
    }
    public int CounterValue(TypeOfUpgrade upgradeName) 
    {
        Debug.Log(upgradesCounter.ContainsKey(upgradeName));
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