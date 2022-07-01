using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using Toolbox;
using UnityEngine.UI;
using Unity.VisualScripting;
using Priority_Queue;

//This script handles running a specific status effect (single) ASSUMING it passed all the checks in order to apply an effect.
public class StatusEffectsRunner : MonoBehaviour
{
    //TODO
    /*
    Make it so there is a red badge on the top right corner of the status effect icon showing the total number of stacked effects (capped at 99. will show "99+" after. If there is only a single stack, don't show anything
    Also make it so there is a different badge on the bottom left corner of the status effect icon showing the maximum current tier the status effect is running. Also make it so if it's tier 1 nothing shows up.

    Investigate new bug with immunity after deactivation.
    */
    // ***



    //Variables Section
    public StatusEffectsScriptObj scriptableObjReference;
    public Image statusEffectImage; //The actual image component, NOT the visible sprite
    GameObject targetToApply;
    AtlasAnimator atlasAnimatorRef;
    ScriptMachine scriptMachineRef;
    StatusEffectsCustomFunctionality customFunctionalityRef;
    SimplePriorityQueue<StatusEffectsExtras> statusEffectQueue = new SimplePriorityQueue<StatusEffectsExtras>();
    private const int effectCountdown = 1;
    public int effectWeight = 0;
    
    public void InitializeEffect(StatusEffectsScriptObj scriptableObjReference, GameObject target)
    {   
        this.scriptableObjReference = scriptableObjReference;
        this.targetToApply = target;
        
        if(scriptableObjReference.isAnimatedActiveIcon == true)
        {
            if (atlasAnimatorRef == null)
            {
                atlasAnimatorRef = GetComponent<AtlasAnimator>();
            }
            atlasAnimatorRef.ReassignAnimation(scriptableObjReference.animatedActiveIcon, scriptableObjReference.activeIconTexture, scriptableObjReference.animationSpeed);
        }
        else
        {
            statusEffectImage.sprite = scriptableObjReference.activeIcon;
        }
    }

    public void InitializePermanentImmunitiy(StatusEffectsScriptObj scriptObjRef)
    {
        effectWeight = 12;
        scriptableObjReference = scriptObjRef;
        statusEffectImage.sprite = scriptableObjReference.permanentImmuneIcon;
    }

    public void ApplyStatusEffect(StatusEffectsInfoCarry infoCarry, UnityAction callback)
    {
        if (atlasAnimatorRef && scriptableObjReference.isAnimatedActiveIcon == true)
        {
            atlasAnimatorRef.enabled = true;
        }
        if (scriptableObjReference.useNormalScripting == false)
        {
            if (scriptMachineRef == null)
            {
                scriptMachineRef = GetComponent<ScriptMachine>();
            }
            scriptMachineRef.graphData = scriptableObjReference.customFunctionality_Visual.graph.CreateData();
            scriptMachineRef.enabled = true;
        }
        else
        {
            if (this.GetComponent(scriptableObjReference.customFunctionality_Script.Type) is StatusEffectsCustomFunctionality customFunc)
            {
                customFunctionalityRef = customFunc;
            }
            else
            {
                customFunctionalityRef = this.gameObject.AddComponent(scriptableObjReference.customFunctionality_Script.Type) as StatusEffectsCustomFunctionality;
            }
            customFunctionalityRef.enabled = true;
        }
        statusEffectQueue.Enqueue(new StatusEffectsExtras(infoCarry.tier, infoCarry.duration, 1), infoCarry.tier); //CHANGE LATER
        effectWeight = statusEffectQueue.First.potency;
        

        if(callback != null)
        {
            StartCoroutine(RunStatusEffect(callback));
        }
    }
    
    public void StartImmunity(float duration, UnityAction callback)
    {
        effectWeight = 11;
        if (atlasAnimatorRef != null)
        {
            atlasAnimatorRef.enabled = false;
        }
        if (scriptMachineRef != null)
        {
            scriptMachineRef.enabled = false;
        }
        if (customFunctionalityRef != null)
        {
            customFunctionalityRef.enabled = false;
        }
        //Show immunity: change image
        StartCoroutine(DelayCallback(duration, callback));
        statusEffectImage.sprite = scriptableObjReference.immuneIcon;
    }

    public IEnumerator DelayCallback(float duration, UnityAction callback)
    {
        yield return new WaitForSeconds(duration);
        if (callback != null)
        {
            callback.Invoke();
        }
    }

    public IEnumerator RunStatusEffect(UnityAction callback)
    {
        while(statusEffectQueue.Count != 0)
        {
            var currentEffect = statusEffectQueue.First;
            effectWeight = currentEffect.potency;

            while (currentEffect.duration > 0)
            {
               if(scriptableObjReference.useNormalScripting == true)
                {
                    currentEffect.duration -= effectCountdown;
                    customFunctionalityRef.RunEffect(currentEffect.potency, targetToApply);
                }
                yield return new WaitForSeconds(effectCountdown);
                currentEffect = statusEffectQueue.First;
            }
            statusEffectQueue.Dequeue();
        }
        if(callback != null){
            callback.Invoke();
        }
    }
}

public class StatusEffectsExtras
//This class contains the information for the effect when running it.
{
    public int potency;
    public float duration;
    public int effectsStacked; //may be used?
    public StatusEffectsExtras(int potency, float duration, int stack = 1)
    {
        this.potency = potency;
        this.duration = duration;
        this.effectsStacked = stack;
    }
}