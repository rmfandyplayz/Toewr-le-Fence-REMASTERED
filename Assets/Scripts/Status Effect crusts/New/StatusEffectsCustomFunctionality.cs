using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class StatusEffectsCustomFunctionality : MonoBehaviour
{
    //Marker Script - This script is a placeholder for custom functionality you want to add to the StatusEffects plugin.
    //This is basically a dummy script
    
    /// <summary>
    /// This function determines what happens every second the effect is run (aka every update cycle).
    /// </summary>
    /// <param name="potency"></param>
    /// <param name="target"></param>
    public virtual void OnEffectUpdate(int potency, GameObject target)
    {
        
    }

    /// <summary>
    /// This function runs once before the effect starts. Useful to save the current state of an enemy before applying an effect and reverting the enemy back when the effect finishes.
    /// </summary>
    /// <param name="target"></param>
    public virtual void OnEffectStart(GameObject target)
    {
        
    }

    /// <summary>
    /// This function runs once after the effect ends. Useful to revert the enemy back to the state it was before the effect was applied.
    /// </summary>
    /// <param name="target"></param>
    public virtual void OnEffectEnd(GameObject target)
    {
        
    }
}
