# Updated Status Effects
## Idea
We will want to find a way to improve the status effect system to allow creating and updating effects to be as simple as possible.

## Problem Description
Currently, the status effect system requires creating a script for the data and functionality section and then generating that using another scriptable object. This can be confusing as to what parts to create to get a status effect working. What we want is to have a single place to generate a generic status effect which can then be modified with additional data and functionality. 

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
4. How does a status effect apply changes to the target object?
5. How should we handle interactions between status effects (e.g. if an enemy is always immune to a status effect, how do we tell the status effect to not apply its effect)?

## General Resources
* Visual Scripting Documentation: https://docs.unity3d.com/Packages/com.unity.visualscripting@1.7