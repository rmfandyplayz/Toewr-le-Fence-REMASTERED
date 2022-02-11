using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Toolbox;

public class CaptureDynamicValue : MonoBehaviour
{
	//Variables section. Try to use [Header("[text]")] to organize the code.
	[SerializeField, NotNull] private Object objectToTween;
	[SerializeField, NotNull] private TweeningScriptObj tweeningScriptObj;
	[SerializeField] private bool applyTweenToChildren = true;
	
	public void RunTweenWithDynamicFloat(float dynamicFloat)
    {
        Tweening_Dynamic_Transfer dynamicValue = new Tweening_Dynamic_Transfer(dynamicFloat);
        tweeningScriptObj.RunTweenOnObjectUsingDynamicValue(objectToTween, dynamicValue);
        if (applyTweenToChildren)
        {
            if(objectToTween is GameObject gameobject && tweeningScriptObj.TryGetType(out var type))
            {
                foreach (var children in gameobject.GetComponentsInChildren(type))
                {
                    tweeningScriptObj.RunTweenOnObjectUsingDynamicValue(children, dynamicValue);
                }
            }
            else if (objectToTween is Component component && tweeningScriptObj.TryGetType(out var _type))
            {
                foreach (var children in component.GetComponentsInChildren(_type))
                {
                    tweeningScriptObj.RunTweenOnObjectUsingDynamicValue(children, dynamicValue);
                }
            }
        }
    }

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
