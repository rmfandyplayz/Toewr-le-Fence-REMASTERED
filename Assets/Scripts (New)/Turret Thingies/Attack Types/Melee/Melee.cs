using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Melee : TurretAttackType
{
	//Variables section. Try to use [Header("[text]")] to organize the code.

	public const string animationSpeed = "speed";
    bool isAttacking = false;
	private GameObject currentTarget;
	private Animator meeleAnimationController;

    //Functions section
    public override void InitializeScript(TurretConfiguration tConfig)
    {
		GameObject animationSpawn;
        base.InitializeScript(tConfig);
		currentTarget = null;
		if(turretSettings.attackTypeIsMeele == true)
        {
			animationSpawn = Instantiate(turretSettings.meeleTurretPrefab, this.transform);
			meeleAnimationController = animationSpawn.GetComponentInChildren<Animator>();
        }
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

    public override void Aim()
    {
        if(isAttacking == true)
        {
            return;
        }
        Vector3 dir = currentTarget.transform.position - transform.position;
        float targetAngle = Vector2.SignedAngle(Vector2.up, dir);
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles,
                                                Vector3.forward * targetAngle,
                                                turretSettings.rotateSpeed.GetUpgradedValue(upgrades.CounterValue(TypeOfUpgrade.Rotation)));
    }

    public override void PerformAttack(float cooldown)
    {
        meeleAnimationController.SetFloat(animationSpeed, 1/cooldown);
        if(currentTarget != null)
        {
            base.PerformAttack(cooldown);
        }
    }
    //Coroutine Section

    protected override IEnumerator AttackCooldown(float cooldown_coroutine)
    {
        meeleAnimationController.Play($"{meeleAnimationController.GetLayerName(0)}.{turretSettings.attackAnimation.name}");
        isAttacking = true;
        yield return base.AttackCooldown(cooldown_coroutine);
        meeleAnimationController.Play($"{meeleAnimationController.GetLayerName(0)}.{turretSettings.idleAnimation.name}");
        isAttacking = false;
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






