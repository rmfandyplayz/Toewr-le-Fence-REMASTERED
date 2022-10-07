using JetBrains.Annotations;
using Priority_Queue;
using System.Collections;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//This script handles running a specific status effect (single) ASSUMING it passed all the checks in order to apply an effect.
public class StatusEffectsRunner : MonoBehaviour
{
    //Variables Section
    public StatusEffectsScriptObj scriptableObjReference;
    public Image statusEffectImage; //The actual image component, NOT the visible sprite
    public TextMeshProUGUI tierText;
    public TextMeshProUGUI stackText;
    GameObject targetToApply;
    AtlasAnimator atlasAnimatorRef;
    StatusEffectsCustomFunctionality customFunctionalityRef;
    [SerializeField] SimplePriorityQueue<StatusEffectsExtras> statusEffectQueue = new SimplePriorityQueue<StatusEffectsExtras>();
    private const int effectCountdown = 1;
    public int effectWeight = 0;
    private float accumulateImmunity; //the total amount of time accumulated in the effect immunity timer
    public UnityEvent OnEffectStart; //Different from the OnEffectStart found in StatusEffectsCustomFunctionality
    public UnityEvent OnTransitionToImmunity;
    public UnityEvent OnEffectEnd; //Different from the OnEffectEnd found in StatusEffectsCustomFunctionality
    private IEnumerator runStatusEffectCoroutine = null; //Used to hold the coroutine RunStatusEffect
    [SerializeField] private int stackCount = 0;

    private void Update()
    {
        stackCount = statusEffectQueue.Count;
        if(statusEffectQueue.Count != 0)
        {
            var currentEffect = statusEffectQueue.First;
        }
    }

    public void InitializeEffect(StatusEffectsScriptObj scriptableObjReference, GameObject target)
    {
        this.scriptableObjReference = scriptableObjReference;
        this.targetToApply = target;
        if (scriptableObjReference.isAnimatedActiveIcon == true)
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
        tierText.transform.parent.gameObject.SetActive(true);
        stackText.transform.parent.gameObject.SetActive(true);
    }

    public void InitializePermanentImmunitiy(StatusEffectsScriptObj scriptObjRef)
    {
        effectWeight = 12;
        scriptableObjReference = scriptObjRef;
        statusEffectImage.sprite = scriptableObjReference.permanentImmuneIcon;
        tierText.transform.parent.gameObject.SetActive(false);
        stackText.transform.parent.gameObject.SetActive(false);
    }

    public void ApplyStatusEffect(StatusEffectsInfoCarry infoCarry, UnityAction callback)
    {
        if (atlasAnimatorRef && scriptableObjReference.isAnimatedActiveIcon == true)
        {
            atlasAnimatorRef.enabled = true;
        }
        if (this.GetComponent(scriptableObjReference.customFunctionality.Type) is StatusEffectsCustomFunctionality customFunc)
        {
            customFunctionalityRef = customFunc;
        }
        else
        {
            customFunctionalityRef = this.gameObject.AddComponent(scriptableObjReference.customFunctionality.Type) as StatusEffectsCustomFunctionality;
        }
        customFunctionalityRef.enabled = true;
        statusEffectQueue.Enqueue(new StatusEffectsExtras(infoCarry.tier, infoCarry.duration, 1), -infoCarry.tier); //CHANGE LATER
        effectWeight = statusEffectQueue.First.potency;

        
        if (callback != null && runStatusEffectCoroutine == null)
        {
            runStatusEffectCoroutine = RunStatusEffect(callback);
            StartCoroutine(runStatusEffectCoroutine);
        }
        stackText.text = statusEffectQueue.Count.ToString();
        tierText.text = ConvertRomanNumeral();
    }

    public void StartImmunity(UnityAction callback)
    {
        effectWeight = 11;
        if (atlasAnimatorRef != null)
        {
            atlasAnimatorRef.enabled = false;
        }
        if (customFunctionalityRef != null)
        {
            customFunctionalityRef.enabled = false;
        }
        //Show immunity: change image
        tierText.transform.parent.gameObject.SetActive(false);
        stackText.transform.parent.gameObject.SetActive(false);
        StartCoroutine(DelayCallback(accumulateImmunity, callback));
        StopCoroutine(runStatusEffectCoroutine);
        runStatusEffectCoroutine = null;
        statusEffectImage.sprite = scriptableObjReference.immuneIcon;
        OnTransitionToImmunity.Invoke();
    }

    public IEnumerator DelayCallback(float duration, UnityAction callback)
    {
        yield return new WaitForSeconds(duration);
        if (callback != null)
        {
            callback.Invoke();
            OnEffectEnd.Invoke();
        }
    }

    public IEnumerator RunStatusEffect(UnityAction callback)
    {
        //temp
        float tempDuration = 1;
        //temp
        accumulateImmunity = 0;
        customFunctionalityRef.OnEffectStart(targetToApply);
        OnEffectStart.Invoke();
        while (statusEffectQueue.Count != 0)
        {
            var currentEffect = statusEffectQueue.First;
            effectWeight = currentEffect.potency;
            while (currentEffect.duration > 0)
            {
                currentEffect.duration -= effectCountdown;
                customFunctionalityRef.OnEffectUpdate(currentEffect.potency, targetToApply);
                if (statusEffectQueue.Count <= 1)
                {
                    stackText.transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    stackText.transform.parent.gameObject.SetActive(true);
                }
                if (effectWeight <= 1)
                {
                    tierText.transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    tierText.transform.parent.gameObject.SetActive(true);
                }
                stackText.text = statusEffectQueue.Count.ToString();
                tierText.text = ConvertRomanNumeral();
                yield return new WaitForSeconds(tempDuration);
                accumulateImmunity += effectCountdown;
                currentEffect = statusEffectQueue.First;
                effectWeight = currentEffect.potency;
            }
            statusEffectQueue.Remove(currentEffect);
        }
        customFunctionalityRef.OnEffectEnd(targetToApply);
        if (callback != null)
        {
            callback.Invoke();
        }
    }

    //helper function for converting the current tier to a roman numeral
    public string ConvertRomanNumeral()
    {
        return effectWeight switch
        {
            1 => "I",
            2 => "II",
            3 => "III",
            4 => "IV",
            5 => "V",
            6 => "VI",
            7 => "VII",
            8 => "VIII",
            9 => "IX",
            10 => "X",
            _ => ""
        };
    }


    /// <summary>
    /// This function stops the running of all status effect events from the Runner.
    /// </summary>
    public void StopAllOperationsR()
    {
        StopAllCoroutines();
    }

}

[System.Serializable]
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