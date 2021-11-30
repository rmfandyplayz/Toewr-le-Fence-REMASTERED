using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Toolbox;

public class StatusEffectHolder : MonoBehaviour, StatusEffectHoldable
{
	SerializedDictionary<string, StatusEffectInformation> effectInformation;


	//Functions section
	public void FinishStatusEffect(StatusEffectFunctionality effect)
    {
		StatusEffectInformation information;
		
		if(effectInformation.TryGetValue(effect.name, out information) == false)
        {
			return;
        }
		information.effectsStacked--;
		information.effectImmunityTimer += effect.ImmuneToEffectTimer();

		if(information.effectsStacked == 0)
        {
			StartCoroutine(ImmunityTimer(information));
        }

    }

	public bool HasEffect<T>()
    {
        if (effectInformation.ContainsKey(typeof(T).ToString()))
		{
			return true;
        }
        else
        {
			return false;
        }
    }

	public void ApplyStatusEffect(StatusEffectFunctionality status)
    {
		StatusEffectInformation information;
		if(status == null)
        {
			return;
        }
		else if(effectInformation.TryGetValue(status.name, out information) == true)
        {
			if(information.isImmunetoEffect == true)
            {
				return;
            }
			information.effectsStacked++;
        }
        else
        {
			information = new StatusEffectInformation();
			information.effectsStacked = 1;
			effectInformation.Add(status.name, information);
        }
		status.RunStatusEffect(this, FinishStatusEffect);
    }
	
	//Coroutines section
	public IEnumerator ImmunityTimer(StatusEffectInformation info)
    {
		info.isImmunetoEffect = true;
		yield return new WaitForSeconds(info.effectImmunityTimer);
		info.isImmunetoEffect = false;
    }

}

[System.Serializable]
public class StatusEffectInformation
{
	public bool isImmunetoEffect;
	public float effectImmunityTimer;
	public int effectsStacked;
}

/*
Quick References:

interface - A class where you can't put variables in it. Can only put functions. (Inherit from multiple interfaces) Think of this like a set of tasks that the interface is able to do.
typeof - Returns object information
abstract - Basically means "it exists, but I'm not going to tell you how it's implemented"
virtual - Allows implimintation inside the function
protected - Lets the abstract class access items within the subclass
static - Allows other classes to use the target content without creating the class
override - Override the function from the previous (inherited) class
base - Call the super class from the previous (inherited) class
cont - Constant; this value will never change

Template lambda function:
[public/private/protected] [bool/float/int/void] [name] => [return statement];

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
