using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUnityEvent;
using UnityEngine.Events;

public class TowerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth=100;
    [SerializeField] 
    private int currentHealth=0;

    public UEventInt OnHealthUpdate;
    public UEventFloat OnHealthRatioUpdate;
    public UnityEvent OnTowerDeath;

    void Start()
    {
        currentHealth = maxHealth;
        OnHealthUpdate?.Invoke(currentHealth);
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
            currentHealth -= 5;
            OnHealthUpdate?.Invoke(currentHealth);
            OnHealthRatioUpdate?.Invoke((float)currentHealth/(float)maxHealth);
        }   
        if(currentHealth <= 0)
        {
            OnTowerDeath?.Invoke();
        } 
    }
}
