using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Toolbox;

public interface StatusEffectHoldable
{
	//Variables section. Try to use [Header("[text]")] to organize the code.
	public bool HasEffect<T>();



	//Functions section
	public void ApplyStatusEffect(StatusEffectFunctionality status);
}

/*
Quick References:

abstract - A template for other classes
virtual - Allows implimintation inside the function
protected - Lets the abstract class access items within the subclass
static - Allows other classes to use the target content without creating the class
override - Override the function from the previous (inherited) class
base - Call the super class from the previous (inherited) class
cont - Constant; this value will never change


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
