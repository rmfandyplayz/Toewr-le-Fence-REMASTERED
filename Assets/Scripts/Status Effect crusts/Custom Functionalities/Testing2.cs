using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing2 : StatusEffectsCustomFunctionality
{
    private float originalSpeed;
    private float newSpeed;

    public override void OnEffectUpdate(int potency, GameObject target)
    {
        newSpeed = originalSpeed - (originalSpeed * potency / 10);
        target.GetComponentInParent<PathMovement>().speed = newSpeed;
    }
    public override void OnEffectStart(GameObject target)
    {
        originalSpeed = target.GetComponentInParent<PathMovement>().speed;
    }
    public override void OnEffectEnd(GameObject target)
    {
        target.GetComponentInParent<PathMovement>().speed = originalSpeed;
    }
}
