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

    /*
    public GameObject normieDamagePrefab;
    public GameObject dankDamagePrefab;
    public GameObject surrealDamagePrefab;
    public GameObject noScopeDamagePrefab;
    */

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
            damagePrefab = ObjectPooling.GetGameObject(damageDictionary[damageInfo.damageType].indicatorPrefab);
        }
        if (damagePrefab != null)
        {
            damagePrefab.transform.position = damageInfo.position;
            damagePrefab.GetComponent<DamageIndicator>().InitializeIndicator(damageInfo.damage, damageInfo.damageType);
        }
    }








}

[System.Serializable]
public class DamageIndicatorPoolInfo
{
    public int startAmount = 75;
    public GameObject indicatorPrefab;
}
