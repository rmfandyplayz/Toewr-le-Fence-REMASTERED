using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUnityEvent;
using UnityEngine.Events;

public class TowerHealth : MonoBehaviour
{
    [SerializeField] float maxBaseHP;
    [SerializeField] float currentBaseHP;
    public UEventFloat OnBaseDamage = new UEventFloat();
    public UnityEvent OnBaseDeath = new UnityEvent();

    private void Awake()
    {
        currentBaseHP = maxBaseHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyController>();
        if(enemy != null)
        {
            if(currentBaseHP > 0)
            {
                currentBaseHP -= enemy.escript.baseDamage;
                OnBaseDamage.Invoke(currentBaseHP/maxBaseHP);
            }
            if(currentBaseHP <= 0)
            {
                OnBaseDeath.Invoke();
            }
        }
    }

}
