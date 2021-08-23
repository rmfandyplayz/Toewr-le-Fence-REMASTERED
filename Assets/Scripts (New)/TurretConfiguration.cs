using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class TurretConfiguration : MonoBehaviour
{
    public TurretSettings tsettings;
    public List<GameObject> targets;
    public GameObject bulletPrefab;
    public List<int> upgradesCounter;
    public List<Transform> firePointList = new List<Transform>();
    private GameObject currentTarget;
    public GameObject temporaryBullet;
    private bool isOnCooldown = false;
    public TurretSpriteHandler spriteHolder; 
    public TurretRangeHandler rangeHolder; 

    public void Initialize()
    {
        name = tsettings.turretName;
        spriteHolder.Initialize(tsettings.turretSprite, tsettings.colliderPositionAndSize);
        rangeHolder.Initialize(tsettings.range);
        if(firePointList.Count == 0)
        {
            foreach(var fp in tsettings.firepointPositionRotation)
            {
                var firePoint = new GameObject("FirePoint");
                firePoint.transform.position = fp.position;
            }
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
                                                tsettings.rotateSpeed);
    }

    IEnumerator Shoot()
    {
        foreach(Transform firePoint in firePointList)
        {
            GameObject bullet = Instantiate(temporaryBullet, firePoint.position, firePoint.rotation);
            bullet.GetComponent<BulletController>().targetenemy = currentTarget;
            //shoot bullet here
            isOnCooldown = true;
            yield return new WaitForSeconds(tsettings.fireRate);
            isOnCooldown = false;
        }
    }



}
