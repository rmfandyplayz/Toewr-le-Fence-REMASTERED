using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ranged : TurretAttackType
{
    //Variables section. Try to use [Header("[text]")] to organize the code.

    private GameObject currentTarget;



    //Functions section

    public override void AddTargetToList(GameObject objectToAdd)
    {
        base.AddTargetToList(objectToAdd);
    }

    public override void RemoveTargetFromList(GameObject objectToRemove)
    {
        base.RemoveTargetFromList(objectToRemove);
    }

    public override void UpdateTargetList()
    {
        float closestEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in targetList)
        {
            if (enemy == null)
            {
                continue;
            }
            float distance = TurretAttackType.FindEnemyDistance(this.transform.position, enemy.transform.position); //temporary variable
            if (distance < closestEnemyDistance)
            {
                closestEnemyDistance = distance;
                currentTarget = enemy;
            }
        }
        if (targetList.Count == 0)
        {
            currentTarget = null;
        }
    }

    public override void PerformAttack(float cooldown)
    {
        if(currentTarget == null) { return; }
        base.PerformAttack(cooldown);
    }

    public override void Aim()
    {
        Vector3 dir = currentTarget.transform.position - transform.position;
        float targetAngle = Vector2.SignedAngle(Vector2.up, dir);
        // Debug.LogWarning(Vector2.SignedAngle(Vector2.up, dir));
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles,
                                                Vector3.forward * targetAngle,
                                                turretSettings.rotateSpeed.GetUpgradedValue(upgrades.CounterValue(TypeOfUpgrade.Rotation)));
    }


    //Coroutine Section

    protected override IEnumerator AttackCooldown(float cooldown_coroutine)
    {
        foreach (Transform firePoint in firePointLocations)
        {
            GameObject bullet = ObjectPooling.GetGameObject(bulletPrefab);
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            BulletController bc = bullet.GetComponent<BulletController>();
            bc.bscript = turretSettings.bullet;
            bc.targetenemy = currentTarget;
            bc.InitUpgrade(upgrades);
        }
        return base.AttackCooldown(cooldown_coroutine);
    }
	
}

/*
Quick References:

To create objects: Instantiate([prefab], [position], [rotation (quaternion)])
To destroy objects: Destroy([gameObject])
Accessing objects: GetComponent<[Script]>()

Physics:
-OnCollisionEnter
-OnCollisionExit
-OnCollisionStay
-OnTriggerEnter
-OnTriggerStay
-OnTriggerExit

Inputs:
Input.GetKeyDown([KeyCode]) - Any key being pressed
Input.GetKeyUp([KeyCode]) - Any key being released
Input.GetKey([KeyCode]) - Any key being pressed & held down

General foreach() format:
foreach([variable type] [variable name] in [target]){}

General for() format:
for([variable type] [variable name] = [starting index]; [variable name] [</>/= (condition)] [target variable to set condition], [variable name] [+/-] [value (step; indicates how much the index moves with the conditions met)]){} 

*/






