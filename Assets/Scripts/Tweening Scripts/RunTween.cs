using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Toolbox;
using Platinio.TweenEngine;
using UnityEngine.Events;

public class RunTween : MonoBehaviour
{
	//Variables section. Try to use [Header("[text]")] to organize the code.
	[SerializeField, NotNull] private Object objectToTween;
	[SerializeField, NotNull] private TweeningScriptObj tweeningScriptObj;
	[SerializeField] private bool applyTweenToChildren = true;
    private TweenInformation currentRunningTween;
    List<int> childTweenID = new List<int>();
    public UnityEvent OnTweenComplete;
	
    //FUNCTIONS SECTION
    
    void RunTweenUniversal(Tweening_Dynamic_Transfer ?dynamicTransfer, int ?varID = null)
    {
        if(currentRunningTween != null && (varID == null || varID.Value != currentRunningTween.currentRunningTween.ID))
        {
            if(tweeningScriptObj.multiType == multipleTypes.runFirstOnly)
            {
                return;
            }
            if (tweeningScriptObj.multiType == multipleTypes.runRecent)
            {
                PlatinioTween.instance.CancelTween(currentRunningTween.currentRunningTween);
                foreach(int childID in childTweenID)
                {
                    PlatinioTween.instance.CancelTween(childID);
                }
                if (currentRunningTween.gotoIndexWhenInterrupted.HasValue)
                {
                    currentRunningTween.nextIndex = currentRunningTween.gotoIndexWhenInterrupted.Value;
                }
                else
                {
                    currentRunningTween = null;
                }
            }
        }
        var previousTween = currentRunningTween;

        currentRunningTween = tweeningScriptObj.RunTweenOnObjectUsingDynamicValue(objectToTween, dynamicTransfer, previousTween);
        if(currentRunningTween != null)
        {
            currentRunningTween.currentRunningTween.SetOnComplete(() => RunTweenUniversal(dynamicTransfer, currentRunningTween.currentRunningTween.ID));
        }
        if (applyTweenToChildren)
        {
            RunTweenOnChildren(dynamicTransfer, previousTween);
        }
    }
    
    #region Public Run Tween Default
    public void RunTweenDefault()
    {
        RunTweenUniversal(null);
    }
    #endregion
    
    public void RunTweenWithDynamicFloat(float dynamicFloat)
    {
        //Tweening_Dynamic_Transfer dynamicValue = new Tweening_Dynamic_Transfer(dynamicFloat);
        RunTweenUniversal(dynamicFloat);
    }
    
    public void RunTweenWithDynamicColor(Color dynamicColor)
    {
        RunTweenUniversal(dynamicColor);
    }
    
    public void RunTweenWithDynamicVector3(Vector3 dynamicVector3)
    {
        RunTweenUniversal(dynamicVector3);
    }

    private void RunTweenOnChildren(Tweening_Dynamic_Transfer? dynamicValue, TweenInformation tweenInformation)
    {
        bool foundValidType = tweeningScriptObj.TryGetType(out var type);
        childTweenID.Clear();
        if (objectToTween is GameObject gameobject && foundValidType)
        {
            foreach (var children in gameobject.GetComponentsInChildren(type))
            {
                var childTween = tweeningScriptObj.RunTweenOnObjectUsingDynamicValue(children, dynamicValue, tweenInformation);
                if(childTween != null)
                {
                    childTweenID.Add(childTween.currentRunningTween.ID);
                }
            }
        }
        else if (objectToTween is Component component && foundValidType)
        {
            foreach (var children in component.GetComponentsInChildren(type))
            {
                var childTween = tweeningScriptObj.RunTweenOnObjectUsingDynamicValue(children, dynamicValue, tweenInformation);
                if (childTween != null)
                {
                    childTweenID.Add(childTween.currentRunningTween.ID);
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
