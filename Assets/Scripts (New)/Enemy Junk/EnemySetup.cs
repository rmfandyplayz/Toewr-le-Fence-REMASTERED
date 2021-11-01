using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;

[CreateAssetMenu(fileName = "Enemy", menuName = "Object/Enemy", order = 1)]
public class EnemySetup : ScriptableObject
{
    public string enemyType;
    public Sprite enemySprite;
    public float speed;
    public float maxHealth;
    public float maxShields;
    //public float firedamageamount;
    //private float firedmgpertick = 1f;
    public int scoreValue;
    public int dropMoneyAmount;
    public bool isBossEnemy = false;
    [BeginIndent]
    [ShowIf(nameof(isBossEnemy), true)] public bool boxShapedSpawningPattern = false;
    [BeginIndent]
        [ShowIf(nameof(isBossEnemy), true)] public int maxEnemies2Spawn;
        [ShowIf(nameof(isBossEnemy), true)] public float firstEnemyOrGroupSpawnDelay;
        [ShowIf(nameof(isBossEnemy), true)] public float enemySpawnFreezeDuration;
        [ShowIf(nameof(isBossEnemy), true)] public float enemySpawnDelay;
    [EndIndent]
    [ShowIf(nameof(boxShapedSpawningPattern), true)] public float enemySpawnGroupSize;
    public float groupSize => boxShapedSpawningPattern ? enemySpawnGroupSize : 1;

}
