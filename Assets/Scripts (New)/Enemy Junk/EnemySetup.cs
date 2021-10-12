using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Object/Enemy", order = 1)]
public class EnemySetup : ScriptableObject
{
    public string enemyType;
    public Sprite enemySprite;
    public float speed;
    public float maxHealth;
    public float maxShields;
    public float firedamageamount;
    public bool isBossEnemy = false;
    //private float firedmgpertick = 1f;
    public int scoreValue;
    public int dropMoneyAmount;




}
