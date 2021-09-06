using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretConfiguration : MonoBehaviour
{
    public TurretSettings tsettings;
    public List<GameObject> targets;
    public GameObject bulletPrefab;
    public SerializedDictionary<TypeOfUpgrade, int> upgradesCounter;
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
        foreach(TypeOfUpgrade upgrade in tsettings.upgrades)
        {
            upgradesCounter.Add(upgrade, 0);
        }      
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
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletController bc = bullet.GetComponent<BulletController>();
            bc.bscript = tsettings.bullet;
            bc.targetenemy = currentTarget;
            bc.InitUpgrade(upgradesCounter);
            
            isOnCooldown = true;
            yield return new WaitForSeconds(tsettings.fireRate.GetUpgradedValue(CounterValue(TypeOfUpgrade.FireRate)));
            isOnCooldown = false;
        }
    }
    public int CounterValue(TypeOfUpgrade upgradeName) 
    {
        return upgradesCounter.ContainsKey(upgradeName)? upgradesCounter[upgradeName] : 0; 
    }
}
