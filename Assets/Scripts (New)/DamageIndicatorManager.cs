using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void GenerateIndicator(damageIndicatorType typeOfDmg)
    {
        switch (typeOfDmg)
        {
            case damageIndicatorType.normieDamage:
                Instantiate(normieDamagePrefab);
                break;
            case damageIndicatorType.dankDamage:
                Instantiate(dankDamagePrefab);
                break;
            case damageIndicatorType.surrealDamage:
                Instantiate(surrealDamagePrefab);
                break;
            case damageIndicatorType.mlgNoScope:
                Instantiate(noScopeDamagePrefab);
                break;
            
        }
    }








}
