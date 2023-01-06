using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum damageIndicatorType
{
    normieDamage, dankDamage, surrealDamage, mlgNoScope
};

public class DamageIndicatorManager : MonoBehaviour
{

    public SerializedDictionary<damageIndicatorType, DamageIndicatorPoolInfo> damageDictionary;

    private void Start()
    {
        foreach (var damagePrefab in damageDictionary.Values)
        {
            ObjectPooling.Preload(damagePrefab.indicatorPrefab, damagePrefab.startAmount);
        }
    }





    public void GenerateIndicator(DamageInfo damageInfo)
    {
        GameObject damagePrefab = null;
        if (damageDictionary.ContainsKey(damageInfo.damageType))
        {
            damagePrefab = ObjectPooling.GetGameObject(damageDictionary[damageInfo.damageType].indicatorPrefab, alreadyActive : false);
        }
        if (damagePrefab != null)
        {
            damagePrefab.GetComponent<DamageIndicator>().InitializeIndicator(damageInfo.damage, damageInfo.damageType, damageInfo.position, damageInfo.velocity);
            damagePrefab.SetActive(true);
        }
    }








}

[System.Serializable]
public class DamageIndicatorPoolInfo
{
    public int startAmount = 75;
    public GameObject indicatorPrefab;
}
