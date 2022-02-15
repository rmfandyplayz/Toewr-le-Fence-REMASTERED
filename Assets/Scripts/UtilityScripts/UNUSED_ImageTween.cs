/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Toolbox;


public class ImageTween : MonoBehaviour
{
    //Variables section. Try to use [Header("[text]")] to organize the code.

    [SerializeField] private Image targetObjectToTween;
    [SerializeField] private TweeningScriptObj tweenScriptObj;
    [SerializeField] private float startingAlphaValue;
    [SerializeField] private float startingFillValue = 1;
    [SerializeField] private float holdTimeUntilStart = 0.5f;
    [SerializeField] private int runningTweenEffects;
    [SerializeField] private bool applyTweenForChildren = true;
    [SerializeField] private bool isRunnerActive = false;

    private void Awake()
    {
        if (targetObjectToTween == null)
        {
            targetObjectToTween = GetComponent<Image>();
        }

        targetObjectToTween?.Fade(startingAlphaValue, 0);
        targetObjectToTween?.FillAmountTween(startingFillValue, 0);

        if (targetObjectToTween != null && applyTweenForChildren == true)
        {
            foreach(Image image in targetObjectToTween.GetComponentsInChildren<Image>())
            {
                image.Fade(startingAlphaValue, 0);
                image.FillAmountTween(startingFillValue, 0);
            }
        }
        isRunnerActive = false;
        StartCoroutine(PauseTween(holdTimeUntilStart, () => isRunnerActive = true));
    }
    public void RunTweenDefault()
    {
        RunTweenOverride(tweenScriptObj);
    }
    
    public void RunTweenWithFillValue(float value)
    {
        tweenScriptObj.helpers.ForEach((x) =>
        {
            if (x.useDynamicValue == true)
            {
                x.dynamicFloat = value;
            }
        });
        RunTweenOverride(tweenScriptObj);
    }

    public void RunTweenOverride(TweeningScriptObj newPreset)
    {
        if(applyTweenForChildren == false)
        {
            return;
        }
        RunTween(newPreset, targetObjectToTween, 0);
        if (targetObjectToTween != null && applyTweenForChildren == true)
        {
            foreach (Image image in targetObjectToTween.GetComponentsInChildren<Image>())
            {
                RunTween(newPreset, image, 0, true);
            }
        }
    }

/// <summary>
/// Run the current tween event from a Tweening Scriptable Object
/// </summary>
/// <remarks>
/// Type: public function
/// </remarks>
/// <param name="tween">TweeningScriptObj: The tween object preset to use.</param>
/// <param name="imageToTween">Image: The image object to apply the effect to.</param>
/// <param name="current">int: The number representing the current tween event</param>
/// <param name="isChild">bool: Whether the image is a child of the main image runner</param>
/// <return> Nothing </return> (if this is not given, it means return nothing (void))

    public void RunTween(TweeningScriptObj tween, Image imageToTween, int current, bool isChild = false)
    {
        if(tween == null || imageToTween == null || isRunnerActive == false)
        {
            return;
        }
        if (current >= tween.helpers.Count)
        {
            if (!isChild)
            {
                runningTweenEffects--;
            }
            return;
        }
        if(current <= 0)
        {
            current = 0;
            if (!isChild)
            {
                runningTweenEffects++;
            }
        }
        var currentTween = tween.helpers[current];
        if(currentTween.disallowMultipleEvents == true && runningTweenEffects > 1)
        {
            if (!isChild)
            {
                runningTweenEffects--;
            }
            return;
        }
        StartTweenEvent(imageToTween, currentTween, ()=> RunTween(tween, imageToTween, current + 1, isChild));
    }

    /// <summary>
    /// Will apply a given tween event to a given image. Runs callback function when the tween is finished.
    /// </summary>
    /// <remarks>
    /// Type: public function
    /// </remarks>
    /// <param name="imageApplied"> Image: The image to apply the effect to</param>
    /// <param name="tween"> TweeningHelper: The tween effect that will be applied.</param>
    /// <param name="callback"> System.Action: A function to run when the tween is completed. </param>
    /// <return> Nothing </return>
    public void StartTweenEvent(Image imageApplied, TweeningHelper tween, System.Action callback)
    {
        if (tween.typeOfTweenEvent == tweenEvents.fade)
        {
            if (tween.useSpeedValue == true)
            {
                imageApplied.FadeAtSpeed(tween.targetValueOfTween, tween.speedValueOfTween).SetEase(tween.ease).SetOwner(this.gameObject).SetOnComplete(callback);
            }
            else
            {
                imageApplied.Fade(tween.targetValueOfTween, tween.timeValueOfTween).SetEase(tween.ease).SetOwner(this.gameObject).SetOnComplete(callback);
            }
        }
        else if (tween.typeOfTweenEvent == tweenEvents.fill)
        {
            if (tween.useSpeedValue == true)
            {
                imageApplied.FillAmountTweenAtSpeed(tween.targetValueOfTween, tween.speedValueOfTween).SetEase(tween.ease).SetOwner(this.gameObject).SetOnComplete(callback);
            }
            else
            {
                imageApplied.FillAmountTween(tween.targetValueOfTween, tween.timeValueOfTween).SetEase(tween.ease).SetOwner(this.gameObject).SetOnComplete(callback);
            }
        }
        else if(tween.typeOfTweenEvent == tweenEvents.cancel)
        {
            imageApplied.gameObject.CancelAllTweens();
            StartCoroutine(PauseTween(tween.timeValueOfTween, callback));
        }
        else
        {
            StartCoroutine(PauseTween(tween.timeValueOfTween, callback));
        }
    }

    //Coroutines section

    /// <summary>
    /// Holds a tween for a given time
    /// </summary>
    /// <remarks>
    /// Type: public coroutine
    /// </remarks>
    /// <param name="timer">float: time to hold the tween.</param>
    /// <param name="callback">System.Action: the function to callback when finished.</param>
    /// <returns>IEnumerator for coroutine</returns>

    public IEnumerator PauseTween(float timer, System.Action callback)
    {
        yield return new WaitForSeconds(timer);
        callback?.Invoke();
    }



}
*/

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
