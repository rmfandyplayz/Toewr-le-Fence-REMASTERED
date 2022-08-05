using System.Collections.Generic;
using UnityEngine;
using Platinio;
using Platinio.TweenEngine;
using UnityEngine.UI;

public enum tweenEvents
{
    fade,
    appear,
    disappear,
    fill,
    move,
    rotate,
    scale,
    color,
    [Tooltip("Pauses animations")] hold, //pauses tweens
    [Tooltip("Stops animations")] cancel //stops tweens
};


[System.Serializable]
public class TweeningHelper
{
    private readonly SerializedDictionary<tweenEvents, System.Func<Object, bool, Tweening_Dynamic_Transfer, bool, float, Platinio.TweenEngine.BaseTween>
> functionSelecter = new SerializedDictionary<tweenEvents, System.Func<Object, bool, Tweening_Dynamic_Transfer, bool, float, Platinio.TweenEngine.BaseTween>> //Based on tweening event type, automatically picks the correct function for the tweening event.
    {
        {tweenEvents.fade, (obj, useRelativeValue, info, useSpeedValue, amountValue)=>
        { 
            var tweeningInfo = info.dynamicValueFloat;
            System.Func<float, float, BaseTween> function = null;
        if(obj is Image img)
            {
                tweeningInfo = useRelativeValue ? img.color.a + tweeningInfo : tweeningInfo;
                function = useSpeedValue ?img.FadeAtSpeed : img.Fade;
            }
        else if(obj is SpriteRenderer spriteRender)
            {
                tweeningInfo = useRelativeValue ? spriteRender.color.a + tweeningInfo : tweeningInfo;
                function = useSpeedValue ?spriteRender.FadeAtSpeed : spriteRender.Fade;
            }  return function?.Invoke(tweeningInfo, amountValue); } },
        //======================================================================
        {tweenEvents.fill, (obj, useRelativeValue, info, useSpeedValue, amountValue)=>
        {
            var tweeningInfo = info.dynamicValueFloat;
            System.Func<float, float, BaseTween> function = null;
            if(obj is Image img)
            {
                tweeningInfo = useRelativeValue ? img.fillAmount + tweeningInfo : tweeningInfo;
                function = useSpeedValue ?img.FillAmountTweenAtSpeed : img.FillAmountTween;
            };
                return function?.Invoke(tweeningInfo, amountValue); } },
        //=======================================================================
        {tweenEvents.move, (obj, useRelativeValue, info, useSpeedValue, amountValue)=> //MOVE
        {
            var tweeningInfo = info.dynamicValueVector3;
            System.Func<Vector3, float, BaseTween> function = null;
            if(obj is GameObject gameObj)
            {
                tweeningInfo = useRelativeValue ? gameObj.transform.position + tweeningInfo : tweeningInfo;
                function = useSpeedValue ?gameObj.MoveAtSpeed : gameObj.Move;
            }
            else if(obj is Component component)
            {
                tweeningInfo = useRelativeValue ? component.transform.position + tweeningInfo : tweeningInfo;
                function = useSpeedValue ?component.gameObject.MoveAtSpeed : component.gameObject.Move;
            }
                return function?.Invoke(tweeningInfo, amountValue); } },
        //=========================================================================
        {tweenEvents.rotate, (obj, useRelativeValue, info, useSpeedValue, amountValue)=> //ROTATE
        {
            var tweeningInfo = info.dynamicValueVector3;
            System.Func<Vector3, float, BaseTween> function = null;
            if(obj is GameObject gameObj)
            {
                tweeningInfo = useRelativeValue ? gameObj.transform.rotation.eulerAngles + tweeningInfo : tweeningInfo;
                function = useSpeedValue ?gameObj.RotateTween : gameObj.RotateTween;
            }
            else if(obj is Component component)
            {
                tweeningInfo = useRelativeValue ? component.transform.rotation.eulerAngles + tweeningInfo : tweeningInfo;
                function = useSpeedValue ?component.gameObject.RotateTween : component.gameObject.RotateTween;
            }
                return function?.Invoke(tweeningInfo, amountValue); } },
        //=========================================================================
        {tweenEvents.scale, (obj, useRelativeValue, info, useSpeedValue, amountValue)=> //SCALE
        {
            var tweeningInfo = info.dynamicValueVector3;
            System.Func<Vector3, float, BaseTween> function = null;
            if(obj is GameObject gameObj)
            {
                tweeningInfo = useRelativeValue ? gameObj.transform.localScale + tweeningInfo : tweeningInfo;
                function = useSpeedValue ?gameObj.ScaleAtSpeed : gameObj.ScaleTween;
            }
            else if(obj is Component component)
            {
                tweeningInfo = useRelativeValue ? component.transform.localScale + tweeningInfo : tweeningInfo;
                function = useSpeedValue ?component.gameObject.ScaleAtSpeed : component.gameObject.ScaleTween;
            }
                return function?.Invoke(tweeningInfo, amountValue); } },
        //=========================================================================
        {tweenEvents.color, (obj, useRelativeValue, info, useSpeedValue, amountValue)=>
        {
            var tweeningInfo = info.dynamicValueColor / 255;
            System.Func<Color, float, BaseTween> function = null;
        if(obj is Image img)
            {
                tweeningInfo = useRelativeValue ? img.color + tweeningInfo : tweeningInfo;
                function = useSpeedValue ?img.ColorTweenAtSpeed : img.ColorTween;
            }
        else if(obj is SpriteRenderer spriteRender)
            {
                tweeningInfo = useRelativeValue ? spriteRender.color + tweeningInfo : tweeningInfo;
                function = useSpeedValue ?spriteRender.ColorTweenAtSpeed : spriteRender.ColorTween;
            }  return function?.Invoke(tweeningInfo, amountValue); } },
        //=========================================================================
        {tweenEvents.appear, (obj, useRelativeValue, info, useSpeedValue, amountValue)=>
        {
            if(obj is GameObject gameObj)
            {
                gameObj.SetActive(true);
            }
            else if(obj is Component component)
            {
                component.gameObject.SetActive(true);
            }
                return null; } },
        //=========================================================================
        {tweenEvents.disappear, (obj, useRelativeValue, info, useSpeedValue, amountValue)=>
        {
            if(obj is GameObject gameObj)
            {
                gameObj.SetActive(false);
            }
            else if(obj is Component component)
            {
                component.gameObject.SetActive(false);
            }
                return null; } }
     };
    private bool toggleOptionsVisibility => typeOfTweenEvent != tweenEvents.appear && typeOfTweenEvent != tweenEvents.disappear && typeOfTweenEvent != tweenEvents.cancel && typeOfTweenEvent != tweenEvents.hold;
    
    private bool toggleDefaultTargetVisibility => toggleOptionsVisibility && !useDynamicValue;

    [SearchableEnum] public tweenEvents typeOfTweenEvent;

    [Tooltip("Choose the type of animation."), ShowIf(nameof(toggleOptionsVisibility), true)]
    public Ease ease;

    [Tooltip("Makes it so this animation block can be resetted to another animation.")]
    public bool canBeInterrupted;

    [ShowIf(nameof(canBeInterrupted), true)]
    public int gotoTweenElement = 0;

    //TODO: Add the ability to reset to a certain element (index)

    [Tooltip("Treat amountValue as a speed value instead of a time value."), ShowIf(nameof(toggleOptionsVisibility), true)] public bool useSpeedValue; //Change to how fast the animation goes instead of a target time value?

    [Tooltip("Prevents values from being hardcoded and makes it flexible. Some scenarios where you would use this include healthbars, which could change. Hardcoding a healthbar does not make sense."), ShowIf(nameof(toggleOptionsVisibility), true)] 
    public bool useDynamicValue; //No hardcoding items

    [Tooltip("If this is checked, it allows for an input of how much to change relative to the current value rather than changing to a fixed value.")]
    public bool useRelativeValue = true;

    [Tooltip("If this option is checked, the next helper will run immediately alongside the current helper.")]
    public bool runNextTweenImmediately;

    [Tooltip("This value uses time in seconds if useSpeedValue is disabled. Otherwise, this value determines how fast the animation will be."), /*ShowIf(nameof(toggleOptionsVisibility), true)*/] 
    [SerializeField] private float _amountValue; //Value for either using speed or time value

    public float amountValue => toggleOptionsVisibility || typeOfTweenEvent == tweenEvents.hold ? _amountValue : 0; //property that returns _amountValue if options are visible

    [Tooltip("Hardcode target values here."), ShowIf(nameof(toggleDefaultTargetVisibility), true), NewLabel("Modify Dynamic Value - Default")] 
    public Tweening_Dynamic_Transfer defaultTarget; //Fallback if dynamicValue does not work

    public BaseTween getTween(Object obj, Tweening_Dynamic_Transfer? dynamicValue)
    {
        var info = useDynamicValue && dynamicValue.HasValue ? dynamicValue.Value : defaultTarget; //Info is a dynamic transfer value; Making a choice between making using default or dynamic value with "?" and "&&." ? and : is basically an if statement. If first expression == true, use the thing after the question mark. Otherwise, use the thing after the colon.

        if(functionSelecter.TryGetValue(typeOfTweenEvent, out var tween))
        {
            return tween(obj, useRelativeValue, info, useSpeedValue, amountValue);
        }
        if(typeOfTweenEvent is tweenEvents.cancel)
        {
            if(obj is GameObject gameObj)
            {
                gameObj.CancelAllTweens();
            }
            else if (obj is Component component)
            {
                component.gameObject.CancelAllTweens();
            }
        }
        return null;
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
