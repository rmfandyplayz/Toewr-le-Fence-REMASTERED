using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

//ACTIVATION OF NEW STATUS EFFECT SYSTEM IS IN BULLETCONTROLLER.CS (at the end of the script)!!!

//This class is used to start a status effect.
//Permanent, temporary, etc.
public class StatusEffectsManager : MonoBehaviour
{
    List<StatusEffectsScriptObj> statusEffectsList = new List<StatusEffectsScriptObj>();
    public GameObject statusEffectPrefab;
    EnemySetup enemySetup;
    [SerializeField] GameObject statusEffectSpriteHolder;

    //Might expand: A variable to hold which state of status effect the enemy is in.

    //TODO
    //Make a system which will handle the potency of effects. (aka I, II, III, IV)

    private void Start()
    {
        enemySetup = GetComponent<EnemyController>().escript;

        foreach (StatusEffectsScriptObj statusEffect in enemySetup.permanentImmunities)
        {
            ApplyPermanentImmunity(statusEffect); //When the enemy spawns, apply any permanent immunities specified.
        }
    }

    public void ApplyPermanentImmunity(StatusEffectsScriptObj statusEffect)
    {
        //Applies any permanent immunities for the enemy when it spawns, if there is any.
        GameObject immunity = ObjectPooling.GetGameObject(statusEffectPrefab);
        immunity.transform.SetParent(statusEffectSpriteHolder.transform);
        immunity.GetComponent<StatusEffectsRunner>().InitializePermanentImmunitiy(statusEffect);
    }

    public void ApplyTemporaryStatusEffect(StatusEffectsScriptObj statusEffect)
    {
        //Checks if there is an immunity
        //Rolls the RNG
        //If passes, it will send signals to StatusEffectRunner to run the chosen status effect.
        
        if (enemySetup.permanentImmunities.Contains(statusEffect))
        {
            return;
        }
        GameObject effect = ObjectPooling.GetGameObject(statusEffectPrefab);
        effect.transform.SetParent(statusEffectSpriteHolder.transform);
        effect.GetComponent<StatusEffectsRunner>().InitializeEffect(statusEffect, this.gameObject);
    }

    public void ApplyTemporaryImmunity(StatusEffectsScriptObj statusEffect)
    {

    }

    public void DeactivateStatusEffect(StatusEffectsScriptObj statusEffect) //Returns status effect to object pool for it to be reused.
    {

    }

}
/*
## Guiding Questions Part 3: Managing Status Effects
1. What is the "API" of the status effect manager (i.e. what functionality does the manager expose to other components, and what is kept private)?
    *  Public
        * Apply Status Effect: 
    * Private
        * 
2. Where does the status effect manager live in the game object (i.e. where is is placed in the hierarchy)?
    * Same place as the enemy controller (we can pass in the current enemy gameobject as the target)
3. How are status effects stored in the manager?
    * Right now its a list of status effect script objects
4. How does the manager create and remove the status effects? What status effect "events" does the manager listen for?
5. How does the manager handle interactions between status effects of the same type? ... Of the same type but different levels? 
    * Same Status Effect and Same Stack/Level: Stacking Additively
    * Same Status Effect but Different Stack/Level: Stacking additively with priority for more potent effects; Less potent effects will freeze and be stored while the more potent effect will run. After the more potent effect finishes, it will run the less potent effect. (TODO: See if there is a priority queue built-in)
*/


/*
    * For Permentant Immunity:
        1. Get the Status Effect Prefab and assign it the perm-immune
        2. Set the prefab (mainly the sprite) to the "Permanent Immune" State in the StatusEffectRunner
*/