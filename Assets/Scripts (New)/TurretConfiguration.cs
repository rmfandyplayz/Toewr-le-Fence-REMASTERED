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
    public float angleOffset = 0;
    private bool isOnCooldown = false;
    public TurretSpriteHandler spriteHolder; 
    public TurretRangeHandler rangeHolder; 

    public void Initialize()
    {
        name = tsettings.turretName;
        spriteHolder.Initialize(tsettings.turretSprite);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyController>()!= null)
        {
            targets.Add(collision.gameObject);
            UpdateTarget();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyController>() != null)
        {
            targets.Remove(collision.gameObject);
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
                Vector3 dir = currentTarget.transform.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.back);
                //Debug.LogWarning(Vector3.SignedAngle(transform.position, dir, Vector3.forward));
                float targetAngle = Vector3.SignedAngle(transform.position, dir, Vector3.forward)-angleOffset;
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles,
                                                     Vector3.forward * targetAngle,
                                                     tsettings.rotateSpeed);
                // transform.eulerAngles = Vector3.forward * (Vector3.SignedAngle(transform.position, dir, Vector3.forward)-angleOffset); 
                //transform.LookAt(currentTarget.transform);
            }
            if(isOnCooldown == false)
            {
                StartCoroutine(Shoot());
            }
        }
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
