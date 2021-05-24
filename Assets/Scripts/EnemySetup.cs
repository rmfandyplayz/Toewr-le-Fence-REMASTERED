using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Object/Enemy", order = 1)]
public class EnemySetup : ScriptableObject
{
    public string enemyType;
    public float speed;
    public float overallHealth;
    public float overallShields;
    public float firedamageamount;
    //private float firedmgpertick = 1f;

    public int dropMoneyAmount;




}
