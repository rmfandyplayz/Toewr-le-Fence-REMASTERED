using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUnityEvent;
using UnityEngine.Events;

public class TowerHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth=100;
    [SerializeField] 
    private float currentHealth=0;

    public UEventInt OnHealthUpdate;
    public UEventFloat OnHealthRatioUpdate;
    public UnityEvent OnTowerDeath;

    void Start()
    {
        currentHealth = maxHealth;
        OnHealthUpdate?.Invoke((int) currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.GetComponent<EnemyTag>() != null )
        {
            if(other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
            var enemy = other.GetComponent<EnemyController>();
            currentHealth -= enemy.currentHealth + enemy.currentShields;
            OnHealthUpdate?.Invoke((int) currentHealth);
            OnHealthRatioUpdate?.Invoke(currentHealth/maxHealth);
        }   
        if(currentHealth <= 0)
        {
            OnTowerDeath?.Invoke();
        } 
    }
}
