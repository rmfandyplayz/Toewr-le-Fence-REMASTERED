﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    public UnityEvent OnEnemyDeath;
    public Rigidbody2D rb;

    private void Start()
    {
        currentHealth = escript.maxHealth;
        currentShields = escript.maxShields;
        setHealth?.Invoke(escript.maxHealth);
        setShields?.Invoke(escript.maxShields);
        //OnHealthUpdate?.Invoke(currentHealth / escript.maxHealth);
        //OnShieldUpdate?.Invoke(currentShields / escript.maxShields);

        GetComponent<SpriteRenderer>().sprite = escript.enemySprite;
        
    }


    public void TakeDamageNormal(float damage)
    {
        TakeDamage(damage, damageIndicatorType.normieDamage);
    }

    public void TakeDamageDank(float damage)
    {
        TakeDamage(damage, damageIndicatorType.dankDamage);
    }

    public void TakeDamageSurreal(float damage)
    {
        TakeDamage(damage, damageIndicatorType.surrealDamage);
    }


    public void TakeDamage(float damage, damageIndicatorType damageType)
    {
        currentShields -= damage;
        float newDamage = 0;

        if (damage == 0)
        {
            return;
        }

        if(currentShields < 0)
        {
            newDamage = Mathf.Abs(currentShields);
            currentShields = 0;
        }
        currentHealth -= newDamage;
        OnHealthUpdate?.Invoke(currentHealth/escript.maxHealth);
        Debug.Log($"{currentHealth / escript.maxHealth}, EnemyController");
        OnShieldUpdate?.Invoke(currentShields/escript.maxShields);
        OnTakeDamage?.Invoke(new DamageInfo(damage, damageType, transform.position));
        if(currentHealth <= 0)
        {
            GoldManager.instance?.AddGold(escript.dropMoneyAmount);
            ScoreManager.scoreManager?.AddScore(escript.scoreValue);
            ObjectPooling.ReturnObject(this.gameObject);
        }
        
    }

    private void OnDisable()
    {
        this.gameObject.CancelAllTweens();
        this.OnEnemyDeath?.Invoke();
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
