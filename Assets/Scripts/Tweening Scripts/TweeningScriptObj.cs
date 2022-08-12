using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Platinio.TweenEngine;
using Platinio;

[CreateAssetMenu(menuName = "Tween Animation")]
public class TweeningScriptObj : ScriptableObject
{
    [Tooltip("How should multiple tweens be handled?")]
    public multipleTypes multiType;
    
    private static readonly SerializedDictionary<string, System.Type> validTweenTargets = new SerializedDictionary<string, System.Type>
    {
        {
            "Image - Fade, Fill, or Color", typeof(Image)
        }, 
        {
            "Sprite Renderer - Fade or Color", typeof(SpriteRenderer)
        }, 
        {
            "Game Object - Transformations, Rotations, Scale, or Set Activation", typeof(GameObject)
        } 
    }; //Has to equal name of image and image type, can add values later on

    public bool TryGetType(out System.Type target)
    {
        return validTweenTargets.TryGetValue(targetType, out target);
    }

    private readonly List<string> validTargetNames = new List<string>
    (
        validTweenTargets.Keys
    ); //Takes each valid target name, gets the keys from dictionary, puts in array

    [Tooltip("Any positive number - Repeat this amount of times\nAny negative number - Repeat infinetly until told to stop (It is recommended to put '-1')\n0 - Off (No repeating)")] 
    public int timesToRepeat = 0;

    [Preset(nameof(validTargetNames))] public string targetType;
    [ReorderableList] public List<TweeningHelper> helpers = new List<TweeningHelper>();

    //FUNCTIONS AREA


    public void RunTweenOnObjectUsingDefaultPreset(Object checkedObject)
    {
        RunTweenOnObjectUsingDynamicValue(checkedObject, null);
    }

    public TweenInformation RunTweenOnObjectUsingDynamicValue(Object checkedObject, Tweening_Dynamic_Transfer? dynamicValue)
    {
        if(checkedObject == null || !validTweenTargets.TryGetValue(targetType, out var checkedType) || checkedObject.GetType() != checkedType)
        {
            return null;
        }
        else
        {
            return RunTweenOnObj(checkedObject, dynamicValue);
        }
    } //Verifies that the object passed in is supported

    public TweenInformation RunTweenOnObj(Object checkedObject, Tweening_Dynamic_Transfer? dynamicValue, int currentIndex = 0, float delay = 0, int currentLoop = 0)
    {
        currentIndex = Mathf.Max(0, currentIndex);

        if (currentIndex >= helpers.Count)
        {
            if (currentLoop == timesToRepeat)
            {
                return null;
            }
            else
            {
                currentIndex = 0;
                currentLoop++;
            }
        }

        var currentHelper = helpers[currentIndex];

        if (currentHelper.getTween(checkedObject, dynamicValue) is BaseTween baseTween)
        {
            baseTween = baseTween.SetDelay(delay).SetEase(currentHelper.ease);
            if (currentHelper.runNextTweenImmediately == false)
            {
                return new TweenInformation
                {
                    currentRunningTween = baseTween, carryDelay = delay, currentLoop = currentLoop, nextIndex = currentIndex++
                };
                //return baseTween.SetOnComplete(() => RunTweenOnObj(checkedObject, dynamicValue, ++currentIndex, 0, currentLoop));
            }
        }
        else
        {
            delay += currentHelper.amountValue;
        }
        return RunTweenOnObj(checkedObject, dynamicValue, ++currentIndex, delay, currentLoop);
    }
}

/// <summary>
/// This enum will represent how the runner will determine how multiple tweens should run. 
/// </summary>
public enum multipleTypes
{
    allowMultiple, //Allows multiple tweens to run at once
    runFirstOnly, //Only run the first tween in the list
    runRecent //Runs the most recent tween in the list
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
