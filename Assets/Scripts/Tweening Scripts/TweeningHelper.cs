using System.Collections.Generic;
using UnityEngine;
using Platinio;
using Platinio.TweenEngine;
using UnityEngine.UI;

public enum tweenEvents
{
    fade,
    fill,
    move,
    rotate,
    scale,
    color,
    hold, //pauses tweens
    cancel //stops tweens
};


[System.Serializable]
public class TweeningHelper
{
    private readonly SerializedDictionary<tweenEvents, System.Func<Object, bool, Tweening_Dynamic_Transfer, float, Platinio.TweenEngine.BaseTween>
> functionSelecter = new SerializedDictionary<tweenEvents, System.Func<Object, bool, Tweening_Dynamic_Transfer, float, Platinio.TweenEngine.BaseTween>> //Based on tweening event type, automatically picks the correct function for the tweening event.
    {
        {tweenEvents.fade, (obj, useSpeedValue, info, amountValue)=>
        { 
        if(obj is Image img)
            {
                return useSpeedValue ?img.FadeAtSpeed(info.dynamicValueFloat, amountValue) : img.Fade(info.dynamicValueFloat,amountValue);
            }
        else if(obj is SpriteRenderer spriteRender)
            {
                return useSpeedValue ?spriteRender.FadeAtSpeed(info.dynamicValueFloat, amountValue) : spriteRender.Fade(info.dynamicValueFloat,amountValue);
            }  return null; } },
            
        {tweenEvents.fill, (obj, useSpeedValue, info, amountValue)=>
        {
            if(obj is Image img)
            {
                return useSpeedValue ?img.FillAmountTweenAtSpeed(info.dynamicValueFloat, amountValue) : img.FillAmountTween(info.dynamicValueFloat,amountValue);
            };
                return null; } },

        {tweenEvents.move, (obj, useSpeedValue, info, amountValue)=>
        {
            if(obj is GameObject gameObj)
            {
                return useSpeedValue ?gameObj.MoveAtSpeed(info.dynamicValueVector3, amountValue) : gameObj.Move(info.dynamicValueVector3,amountValue);
            }
                return null; } },

        {tweenEvents.rotate, (obj, useSpeedValue, info, amountValue)=>
        {
            if(obj is GameObject gameObj)
            {
                return gameObj.RotateTween(info.dynamicValueVector3, amountValue);
            }
                return null; } },

        {tweenEvents.scale, (obj, useSpeedValue, info, amountValue)=>
        {
            if(obj is GameObject gameObj)
            {
                return useSpeedValue ?gameObj.ScaleAtSpeed(info.dynamicValueVector3, amountValue) : gameObj.ScaleTween(info.dynamicValueVector3,amountValue);
            }
                return null; } },

        {tweenEvents.color, (obj, useSpeedValue, info, amountValue)=>
        {
            if(obj is Image img)
            {
                return useSpeedValue ?img.ColorTweenAtSpeed(info.dynamicValueColor, amountValue) : img.ColorTween(info.dynamicValueColor,amountValue);
            }
        else if(obj is SpriteRenderer spriteRender)
            {
                return useSpeedValue ?spriteRender.ColorTweenAtSpeed(info.dynamicValueColor, amountValue) : spriteRender.ColorTween(info.dynamicValueColor,amountValue);
            }  return null; } }
     };

    [SearchableEnum] public tweenEvents typeOfTweenEvent;
    public bool useSpeedValue; //Change to how fast the animation goes instead of a target time value?
    [Tooltip("Prevents values from being hardcoded and makes it flexible. Some scenarios where you would use this include healthbars, which could change. Hardcoding a healthbar does not make sense.")] public bool useDynamicValue; //No hardcoding items
    public Ease ease;
    [Tooltip("This value uses time in seconds if useSpeedValue is disabled. Otherwise, this value determines how fast the animation will be.")] public float amountValue; //Value for either using speed or time value
    [Tooltip("Hardcode target values here."), HideIf(nameof(useDynamicValue), true)] public Tweening_Dynamic_Transfer defaultTarget; //Fallback if dynamicValue does not work


    public BaseTween getTween(Object obj, Tweening_Dynamic_Transfer dynamicValue)
    {
        var info = useDynamicValue ? dynamicValue : defaultTarget;
        if(info != null && functionSelecter.TryGetValue(typeOfTweenEvent, out var tween))
        {
            return tween(obj, useSpeedValue, info, amountValue);
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
