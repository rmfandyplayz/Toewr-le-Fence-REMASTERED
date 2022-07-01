using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class StatusEffectsCustomFunctionality : MonoBehaviour
{
    //Marker Script - This script is a placeholder for custom functionality you want to add to the StatusEffects plugin.
    //This is basically a dummy script
    
    public virtual void RunEffect(int potency, GameObject target)
    {
        //Debug.LogError($"Potency - {potency}, from StatusEffectsCustomFunctionality (Script)");
    }

    //TODO: Add a mechanism to get the current highest potency of the effects list.
}
