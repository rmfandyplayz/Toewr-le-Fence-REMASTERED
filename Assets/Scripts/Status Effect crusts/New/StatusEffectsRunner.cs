using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;
using UnityEngine.UI;
using Unity.VisualScripting;

//This script handles all the static effects functionalities
public class StatusEffectsRunner : MonoBehaviour
{
    //Variables Section
    public StatusEffectsScriptObj scriptableObjReference;
    public Image statusEffectImage; //The actual image component, NOT the visible sprite
    GameObject targetToApply;
    AtlasAnimator atlasAnimator;
    
    public void InitializeEffect(StatusEffectsScriptObj scriptableObjReference, GameObject target)
    {
        
        this.scriptableObjReference = scriptableObjReference;
        this.targetToApply = target;
        if(scriptableObjReference.isAnimatedActiveIcon == true)
        {
            //statusEffectImage.gameObject = scriptableObjReference.animatedActiveIcon;

            //TO DO:
            //Add AtlasAnimator integration to this game object on the same level as this script
        }
        else
        {
            statusEffectImage.sprite = scriptableObjReference.activeIcon;
        }
    }

    public void OnEnable() //Passed the if check, this function handles the running of the status effect
    {
        /*
On apply:
-The icon for the status effect will be placed in the grid layout with a simple pop in animation
-If there are multiple icons, there will be a priority system that will determine which types of icons display first
?Generally, if an enemy has a permenant immunity to a certain effect, that will always be one of the first icons to appear.
?If an enemy has a temporary immunity to a certain effect, that will be the second priority icon to appear.
?Otherwise, all active effect icons will be displayed with the higher stacking icons coming first.
         */

        //Assuming it is already placed correctly

        //Check if scriptableObject reference is using Visual Scripting or Regular scripting. 
        //If using Visual Scripting, we run the visual script using script machine component
        //If using regular scripting, we simply just add the component to the game object.
        if(scriptableObjReference.useNormalScripting == false)
        {
            //GetComponent<ScriptMachine>().graph.;
            
            // Get the Script Machine component
            // Assign the graph to the script machine
        }
        else
        {
            //this.AddComponent(scriptableObjReference.customFunctionality_Script.Type);
            
            // Add component from the custom functionaly script type
            // TODO: Check if the script automatically updates
        }
        
    }

    public void OnDisable()
    {
        
    }

}

/*
# Updated Status Effects
## Idea
We will want to find a way to improve the status effect system to allow creating and updating effects to be as simple as possible.

## Problem Description
Currently, the status effect system requires creating a script for the data and functionality section and then generating that using another scriptable object. This can be confusing as
to what parts to create to get a status effect working. What we want is to have a single place to generate a generic status effect which can then be modified with additional data and
functionality. 

## Questions to help guide solving the problem
1. What should the process for creating a new effect be like?
    * Create a single scriptable object 
    * Add functionality using script (C# or visual)
2. How many components/scriptable objects should be used to define what a status effect is and how it works? (i.e. How modular should the effects system be?) 
    * 2 modules: A scriptable object for holding information and script for functionality
3. For the data, what are all the common information used by scriptable objects and what is specific to each?
    * Common data: 
        * Name
        * Icon (may be animated)
            * Active Icon
            * Immune
        * Particles
        * Presets: Commonly used info about object (speed, health, etc).
        * Custom Functionality:  (SerializeType) Script or (ScriptGraphAsset) Visual Script -> Anything not part of presets
    * Specific data is part of custom functionality.
4. For functionality, what are the general patterns for how the status effect code will be ran?
    * No real patterns, depends on status effect
5. How does the status effect get activated and deactivated, and what states can a status effect be in?
    * NULL: Status effect not applied
    * Activate: Status effect is running
    * Deactivate (Immune): Status effect is stopped and cannot run for some time

## Guiding Questions Part 2: How does the status effect run?
1. Which game object(s) does the status effect spawn on?
    * Spawn under status effect object (Enemy > Blueprint Enemy > Status Effect Canvas > Status Effect)
2. What components cause the status effect to active/deactive?
    * Status effect Runner script that is part of the "Status Effect Square".
3. How do we pass the target information to the status effect? 
    * When initializing the status effect, we also pass in the targeted GameObject


4. How does a status effect apply changes to the target object? (How are status effects applied)

-On hitting enemy:
-First, an if check will run to see if the enemy is immune to the status effect.
-If not, RNG script will run.
-If the RNG hits, the status effect will be applied.
On apply:
-The icon for the status effect will be placed in the grid layout with a simple pop in animation
-If there are multiple icons, there will be a priority system that will determine which types of icons display first
?Generally, if an enemy has a permenant immunity to a certain effect, that will always be one of the first icons to appear.
?If an enemy has a temporary immunity to a certain effect, that will be the second priority icon to appear.
?Otherwise, all active effect icons will be displayed with the higher stacking icons coming first.
On stacking:
-A little notification-like icon will appear on the active effect icon with the number of stacks with a pop in animation. The stacking number will cap at 99 and will say "99+" if there are more than 99 stacks.
-The list reorders with the higher stacking effects first
??Generally, whenever something happens to the effects list, the reorder function will run to reorder. If priority can't take place and the ordering is random, do NOT reorder the random icons. Generally, permanent immunity icons will not experience reorder.
On status effect running out:
?A dissolve transition will take place that will make the active icon turn into an immune icon. The list reorders.
!!! While reordering, an animation will take place as well. It will most likely be a slide/move transition.
On immunity running out:
?The status effect will be removed from the list with a pop out animation.
?The list reorders


5. How should we handle interactions between status effects (e.g. if an enemy is always immune to a status effect, how do we tell the status effect to not apply its effect)?
    * Between Permentant immunity and Effects: Immunity cancels out effect
    * 
## General Resources
* Visual Scripting Documentation: https://docs.unity3d.com/Packages/com.unity.visualscripting@1.7
 */