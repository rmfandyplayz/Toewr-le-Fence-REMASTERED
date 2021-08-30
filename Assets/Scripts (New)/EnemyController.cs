using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUnityEvent;

public class EnemyController : MonoBehaviour
{
    public float currentHealth;
    public float currentShields;
    public EnemySetup escript;
    public UEventFloat setHealth;
    public UEventFloat setShields;
    public UEventFloat OnHealthUpdate;
    public UEventFloat OnShieldUpdate;
    public UEventDamageInfo OnTakeDamage;

    private void Start()
    {
        currentHealth = escript.maxHealth;
        currentShields = escript.maxShields;
        setHealth?.Invoke(escript.maxHealth);
        setShields?.Invoke(escript.maxShields);
        OnHealthUpdate?.Invoke(currentHealth);
        OnShieldUpdate?.Invoke(currentShields);

        GetComponent<SpriteRenderer>().sprite = escript.enemySprite;


    }



    public void TakeDamage(float damage)
    {
        OnTakeDamage?.Invoke(new DamageInfo(damage, damageIndicatorType.dankDamage, transform.position));
        currentShields -= damage;
        float newDamage = 0;
        if(currentShields < 0)
        {
            newDamage = Mathf.Abs(currentShields);
            currentShields = 0;
        }
        currentHealth -= newDamage;
        OnHealthUpdate?.Invoke(currentHealth);
        OnShieldUpdate?.Invoke(currentShields);
        if(currentHealth <= 0)
        {
            GoldManager.instance?.AddGold(escript.dropMoneyAmount);
            ScoreManager.scoreManager?.AddScore(escript.scoreValue);
            Destroy(this.gameObject);
        }
        
    }



}

public class DamageInfo
{
    public float damage;
    public damageIndicatorType damageType;
    public Vector3 position;

    public DamageInfo(float dmg, damageIndicatorType dit, Vector3 pos)
    {
        damage = dmg;
        damageType = dit;
        position = pos;
    }

}
