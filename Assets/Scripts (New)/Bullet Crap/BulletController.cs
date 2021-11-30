using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletSetup bscript;
    public GameObject targetenemy;
    private Camera main;
    public SerializedDictionary<TypeOfUpgrade, int> upgradesCounter;
    public int getCounter(TypeOfUpgrade upgradeName) => upgradesCounter.ContainsKey(upgradeName) ? upgradesCounter[upgradeName] : 0;

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
            //if(upgradesCounter.ContainsKey(pair.Key))
            //{
                upgradesCounter[pair.Key] = pair.Value.Counter;
            //}
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
            ObjectPooling.ReturnObject(this.gameObject);
        }

        if (screenPosition.x > Screen.width || screenPosition.x < 0)
        {
            ObjectPooling.ReturnObject(this.gameObject);
        }



    }

    private void ApplyDamage(EnemyController enemy, float damage, damageIndicatorType type)
    {
        damage = Mathf.CeilToInt(bscript.explosionDamagePercent / 100 * damage);
        foreach(var Enemy in Physics2D.OverlapCircleAll(enemy.transform.position , bscript.explosionRadius))
        {

            if(Enemy.GetComponent<EnemyController>() is EnemyController controller && controller != enemy)
            {
                controller.TakeDamage(damage, type);
            }
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
            var damageType = bscript.DamageCalculation(ref damage, upgradesCounter);
            enemy.TakeDamage(damage, damageType);
            ApplyDamage(enemy, damage, damageType);
            ObjectPooling.ReturnObject(this.gameObject);
        }
        if(collision.gameObject.GetComponent<StatusEffectHoldable>() is StatusEffectHoldable name)
        {
            foreach (var effects in bscript.statusEffects)
            {
                name.ApplyStatusEffect(effects.CreateStatusEffect(collision.gameObject));
            }
        }
    }

    



}