using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class TurretAttackType : MonoBehaviour
{
	//Variables section. Try to use [Header("[text]")] to organize the code.

	protected List<GameObject> targetList = new List<GameObject>();
	protected List<Transform> firePointLocations = new List<Transform>();
	protected bool isAttackOnCooldown = false;
	protected TurretSettings turretSettings;
	protected SerializedDictionary<TypeOfUpgrade, UpgradeCounterInfo> upgrades;
	protected GameObject bulletPrefab; //temporary variable

	//Functions section
    public virtual void InitializeScript(TurretConfiguration tConfig)
    {
		turretSettings = tConfig.tsettings;
		upgrades = tConfig.upgradesCounter;
		bulletPrefab = tConfig.bulletPrefab;

        foreach (var type in turretSettings.firepointPositionRotation)
        {
			GameObject firePoint = new GameObject();
			firePoint.transform.SetParent(this.transform);
			firePoint.transform.position = type.position;
			firePoint.transform.eulerAngles = Vector3.forward*type.value;
			firePointLocations.Add(firePoint.transform);
		}
    }
	
	public virtual void UpdateTargetList()
    {
		return;
    }

	public virtual void AddTargetToList(GameObject objectToAdd)
    {
		if(objectToAdd.GetComponent<EnemyController>() != null)
        {
			targetList.Add(objectToAdd);
			UpdateTargetList();
        }
    }

	public virtual void RemoveTargetFromList(GameObject objectToRemove)
    {
        if (targetList.Contains(objectToRemove))
        {
			targetList.Remove(objectToRemove);
			UpdateTargetList();
        }
    }

	public virtual void PerformAttack(float cooldown)
    {
		if(isAttackOnCooldown == false)
        {
			StartCoroutine(AttackCooldown(cooldown));
        }
    }

	public virtual void Aim()
	{
		return;
	}

	public static float FindEnemyDistance(Vector2 towerPosition, Vector2 enemyPos)
	{
		return (towerPosition - enemyPos).magnitude;
	}

	//Coroutine Section

	virtual protected IEnumerator AttackCooldown(float cooldown_coroutine)
    {
		isAttackOnCooldown = true;
		yield return new WaitForSeconds(cooldown_coroutine);
		isAttackOnCooldown =	false;
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
*/






