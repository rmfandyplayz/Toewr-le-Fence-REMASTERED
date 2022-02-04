using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MeleeDamager : MonoBehaviour
{
	//Variables section. Try to use [Header("[text]")] to organize the code.
	public float damage;

    //Functions section
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.GetComponent<EnemyController>() is EnemyController controller)
        {
            controller.TakeDamage(damage, damageIndicatorType.normieDamage);
        }
    }


    //Coroutine Section
	
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






