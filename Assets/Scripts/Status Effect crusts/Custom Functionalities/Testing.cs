using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : StatusEffectsCustomFunctionality
{
    public override void OnEffectUpdate(int potency, GameObject target)
    {
        target.GetComponentInParent<EnemyController>().TakeDamage(2 * potency, damageIndicatorType.dankDamage);
    }
}
