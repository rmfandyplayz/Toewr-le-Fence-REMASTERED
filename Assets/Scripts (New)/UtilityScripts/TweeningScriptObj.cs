using System.Collections.Generic;
using UnityEngine;

public enum tweenEvents
{
    fade,
    fill,
    hold, //pauses tweens
    cancel //stops tweens
};


[CreateAssetMenu(menuName = "Tween Animation")]
public class TweeningScriptObj : ScriptableObject
{
    public List<TweeningHelper> helpers = new List<TweeningHelper>();
}

[System.Serializable]
public class TweeningHelper
{
    public tweenEvents typeOfTweenEvent;
    public bool disallowMultipleEvents;
    public bool useSpeedValue; //Change to how fast the animation goes instead of a target time value?
    public bool useDynamicValue; //No hardcoding items
    [ShowIf(nameof(useSpeedValue), true)] public float speedValueOfTween;
    [ShowIf(nameof(useSpeedValue), false)] public float timeValueOfTween;
    [HideIf(nameof(useDynamicValue), true)] public float targetValueOfTween;
    public Ease ease;
    public float dynamicFloat
    {
        //Dynamically sets/gets values
        get
        {
            return targetValueOfTween;
        }
        set
        {
            if (useDynamicValue)
            {
                targetValueOfTween = value;
                Debug.LogWarning(value);
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
