using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float currentHealth;
    private float currentShields;
    public EnemySetup escript;

    private void Start()
    {
        currentHealth = escript.maxHealth;
        currentShields = escript.maxShields;
    }



    public void TakeDamage(float damage)
    {
        currentShields -= damage;
        float newDamage = 0;
        if(currentShields < 0)
        {
            newDamage = Mathf.Abs(currentShields);
            currentShields = 0;
        }
        currentHealth -= newDamage;

        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        Debug.Log(damage);
    }




}
