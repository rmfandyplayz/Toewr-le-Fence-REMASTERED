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
    public GameObject normieDamagePrefab;
    public GameObject dankDamagePrefab;
    public GameObject surrealDamagePrefab;
    public GameObject noScopeDamagePrefab;

    public void GenerateIndicator(DamageInfo damageInfo)
    {
        GameObject damagePrefab = null;
        switch (damageInfo.damageType)
        {
            case damageIndicatorType.normieDamage:
                damagePrefab = normieDamagePrefab;
                break;
            case damageIndicatorType.dankDamage:
                damagePrefab = dankDamagePrefab;
                break;
            case damageIndicatorType.surrealDamage:
                damagePrefab = surrealDamagePrefab;
                break;
            case damageIndicatorType.mlgNoScope:
                damagePrefab = noScopeDamagePrefab;
                break;
            
        }
        if (damagePrefab != null)
        {
            Instantiate(damagePrefab, damageInfo.position, Quaternion.identity).GetComponent<DamageIndicator>().InitializeIndicator(damageInfo.damage, damageInfo.damageType);
        }
    }








}
