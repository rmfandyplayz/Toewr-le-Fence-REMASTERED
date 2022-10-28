using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

//ACTIVATION OF NEW STATUS EFFECT SYSTEM IS IN BULLETCONTROLLER.CS (at the end of the script)!!!

//This class is used to start a status effect.
//Permanent, temporary, etc.
public class StatusEffectsManager : MonoBehaviour
{
    List<StatusEffectsRunner> statusEffectsList = new List<StatusEffectsRunner>();
    List<StatusEffectsScriptObj> temporaryImmuneList = new List<StatusEffectsScriptObj>();
    public GameObject statusEffectPrefab;
    public EnemySetup enemySetup;
    [SerializeField] GameObject statusEffectSpriteHolder;

    private void Start()
    {
        enemySetup = GetComponentInParent<EnemyController>().escript;

        foreach (StatusEffectsScriptObj statusEffect in enemySetup.permanentImmunities)
        {
            ApplyPermanentImmunity(statusEffect); //When the enemy spawns, apply any permanent immunities specified.
        }
    }

    private void OnDisable()
    {
        enemySetup = null;
    }


    /// <summary>
    /// Applies any permanent immunities to the enemy, if any is declared within its Scriptable Object.
    /// </summary>
    /// <param name="statusEffect"></param>
    public void ApplyPermanentImmunity(StatusEffectsScriptObj statusEffect)
    {
        //Applies any permanent immunities for the enemy when it spawns, if there is any.
        GameObject immunity = ObjectPooling.GetGameObject(statusEffectPrefab);
        var immuneGetComp = immunity.GetComponent<StatusEffectsRunner>();
        immuneGetComp.InitializePermanentImmunitiy(statusEffect);
        immunity.transform.SetParent(statusEffectSpriteHolder.transform);
        
        statusEffectsList.Add(immuneGetComp);
    }

    public bool hasImmunity(StatusEffectsScriptObj statusScriptObj)
    {
        if(enemySetup == null)
        {
            //Debug.LogError($"Enemy Setup Step 1: {enemySetup}");
            //Debug.LogError($"Enemy Setup Step 2: {GetComponentInParent<EnemyController>()}");
            enemySetup = GetComponentInParent<EnemyController>().escript;
            //Debug.LogError($"Enemy Setup Step 3: {enemySetup}");
        }
        return (enemySetup.permanentImmunities?.Contains(statusScriptObj) ?? false) || (temporaryImmuneList?.Contains(statusScriptObj) ?? false);
    }
    
    //May use later. Currently has no use. ---
    public void ApplyTemporaryStatusEffect(StatusEffectsInfoCarry infoCarry)
    {
        if (hasImmunity(infoCarry.statusEffect) == true)
        {
            return;
        }
        GameObject effect = ObjectPooling.GetGameObject(statusEffectPrefab);
        effect.transform.SetParent(statusEffectSpriteHolder.transform);
        effect.GetComponent<StatusEffectsRunner>().InitializeEffect(infoCarry.statusEffect, this.gameObject);
    }
    //--- Prob won't use

    public void ApplyTemporaryStatusEffect_NoCheck(StatusEffectsInfoCarry infoCarry)
    {
        //If there is an existing running status effect, add time
        foreach(StatusEffectsRunner statusEffect in statusEffectsList)
        {
            if (statusEffect.scriptableObjReference == infoCarry.statusEffect)
            {
                statusEffect.ApplyStatusEffect(infoCarry, null);
                return;
            }
        }

        //If not, make a new one
        GameObject effect = ObjectPooling.GetGameObject(statusEffectPrefab, statusEffectSpriteHolder.transform);
        effect.transform.SetParent(statusEffectSpriteHolder.transform);
        StatusEffectsRunner statusEffectsRunner = effect.GetComponent<StatusEffectsRunner>();
        statusEffectsRunner.InitializeEffect(infoCarry.statusEffect, this.gameObject);
        statusEffectsList.Add(statusEffectsRunner);
        statusEffectsRunner.ApplyStatusEffect(infoCarry, callback: () => { ApplyTemporaryImmunity(infoCarry); });
        ReorderEffectsList();
    }

    /// <summary>
    /// Applies immunity to an effect temporarily for however many seconds the enemy got the effect for.
    /// </summary>
    /// <param name="infoCarry"></param>
    public void ApplyTemporaryImmunity(StatusEffectsInfoCarry infoCarry)
    {
        temporaryImmuneList.Add(infoCarry.statusEffect);
        foreach (StatusEffectsRunner effect in statusEffectsList)
        {
            if (effect.scriptableObjReference == infoCarry.statusEffect)
            {
                //if there is a tween running, don't fully deactivate the effect until the tween finish running.
                effect.StartImmunity(() => { DeactivateStatusEffect(infoCarry); });
                break;
            }
        }
        ReorderEffectsList();
    }

    public void DeactivateStatusEffect(StatusEffectsInfoCarry infoCarry) //Returns status effect to object pool for it to be reused.
    {
        temporaryImmuneList.Remove(infoCarry.statusEffect);
        foreach (StatusEffectsRunner effect in statusEffectsList)
        {
            if (effect.scriptableObjReference == infoCarry.statusEffect)
            {
                //Debug.Log(effect.gameObject);
                //Take information from the info carry. Most notably, the length of the animation so we can hold off returning the gameObject to the object pool.
                ObjectPooling.ReturnObject(effect.gameObject);
                statusEffectsList.Remove(effect);
                break;
            }
        }
        ReorderEffectsList();
    }

    public void ReorderEffectsList()
    {
        //Skip the count of permanent immunities. Don't reshuffle them
        //

        /*
        We have a list of temporary immunities and a list of active ones.
        Reorder the visible child objects under the manager (StatusEffects object in the scene)
        */

        /*
        foreach(Transform child in this.transform)
        {
            child.gameObject.transform.SetSiblingIndex(0);
            
        }
        */

        
        statusEffectsList.Sort((x, y) => { return y.effectWeight - x.effectWeight; });
        //Lambda function: x and y are any two StatusEffectRunner's that are being compared. 
        for (int i = enemySetup.permanentImmunities.Count; i < statusEffectsList.Count; i++)
        {
            statusEffectsList[i].gameObject.transform.SetSiblingIndex(i);
        }

        //We can shuffle the temporary immunities independent from the active immunities.
        //We also need to keep track of the index number.

    }

    /// <summary>
    /// Stops the running of any active status effect, immunity, etc from the Manager.
    /// </summary>
    public void StopAllOperationsM()
    {
        statusEffectsList.ForEach((effect) => effect.StopAllOperationsR());
    }
    
}