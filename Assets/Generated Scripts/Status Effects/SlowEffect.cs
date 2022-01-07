using System.Collections; 
using UnityEngine; 
using UnityEngine.Events; 
using Toolbox;
 
[CreateAssetMenu(fileName = "New SlowEffect", menuName = 
"Status Effect/SlowEffect")] 
public class SlowEffect : StatusEffectCreation<SlowEffect_Data, SlowEffect_Functionality>{}
 
[System.Serializable] 
public class SlowEffect_Data: StatusEffectCenter
{ 
   // Put additional data here for the status effect 
   // This class inherits from StatusEffectCenter; includes effectChance, effectDuration, and effectLevel

} 
 
public class SlowEffect_Functionality: StatusEffectFunctionality <SlowEffect_Data>
{ 
    public override float ImmuneToEffectTimer()
    {
        // return the immunity timer amount 
        return statusEffectData.effectDuration;
    } 
 
    public override void RunStatusEffect(StatusEffectHoldable statusEffectHolder, UnityAction<StatusEffectFunctionality> callback)
    {
        if(RNG.Chance(statusEffectData.effectChance) == true && statusEffectHolder is StatusEffectHolder holder)
        {
            holder.StartCoroutine(SlowEffect(callback));
        }
    } 
 
    // Put additional helper functions/coroutines here 
    public IEnumerator SlowEffect(UnityAction<StatusEffectFunctionality> callback)
    {
        targetOfStatusEffect.GetComponentInParent<PathMovement>().ApplySlow(statusEffectData.effectLevel);
        yield return new WaitForSeconds(statusEffectData.effectDuration);
        targetOfStatusEffect.GetComponentInParent<PathMovement>().RemoveSlow(statusEffectData.effectLevel);
        callback.Invoke(this);
    }

}