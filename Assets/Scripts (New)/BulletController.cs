using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletSetup bscript;
    public GameObject targetenemy;
    private Camera main;
    public SerializedDictionary<TypeOfUpgrade, int> upgradesCounter;

    void Start()
    {
        main = Camera.main;
        foreach(TypeOfUpgrade upgrade in bscript.upgrades)
        {
            upgradesCounter.Add(upgrade, 0);
        } 
    }

    public void InitUpgrade(SerializedDictionary<TypeOfUpgrade, UpgradeCounterInfo> counter)
    {
        foreach (var pair in counter)
        {
            if(upgradesCounter.ContainsKey(pair.Key))
            {
                upgradesCounter[pair.Key] = pair.Value.Counter;
            }
        }
    }

    void Update()
    {
        if(bscript.followTarget == true && targetenemy != null)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, targetenemy.transform.position, bscript.speed * Time.deltaTime);
            //transform.LookAt(targetenemy.transform.position, Vector3.up);
            float angle = 0;

            Vector3 relative = transform.InverseTransformPoint(targetenemy.transform.position);
            angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
            transform.Rotate(0, 0, -angle);

        }
        else
        {
            this.transform.position += transform.up * bscript.speed * Time.deltaTime;
        }

        Vector2 screenPosition = main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            Destroy(this.gameObject);
        }

        if (screenPosition.x > Screen.width || screenPosition.x < 0)
        {
            Destroy(this.gameObject);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {

            float damage = bscript.bulletDamage.GetUpgradedValue(upgradesCounter.ContainsKey(TypeOfUpgrade.BulletDamage)
                                                                                            ? upgradesCounter[TypeOfUpgrade.BulletDamage] 
                                                                                            : 0);
            var damageType = DamageCalculation(ref damage);
            enemy.TakeDamage(damage, damageType);
            Destroy(this.gameObject);
        }
    }

    public damageIndicatorType DamageCalculation(ref float damage)
    {
        int RNG = Random.Range(0, 100);
        if(bscript.NoscopeDmgChance > RNG)
        {
            damage = bscript.noscopeDmg;
            return damageIndicatorType.mlgNoScope;
        }
        else if(bscript.surrealDmgChance > RNG - bscript.NoscopeDmgChance)
        {
            damage *= bscript.surrealDmgMultiplier;
            return damageIndicatorType.surrealDamage;
        }
        else if(bscript.dankDmgChance > RNG - bscript.NoscopeDmgChance - bscript.surrealDmgChance)
        {
            damage *= bscript.dankDmgMultiplier;
            return damageIndicatorType.dankDamage;
        }
        else
        {
            damage = damage;
            return damageIndicatorType.normieDamage;
        }
    }



}