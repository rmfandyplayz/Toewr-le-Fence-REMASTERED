using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Object/Bullet")]
public class BulletSetup : ScriptableObject
{
    public float speed = 70f;
    public float bulletDamage = 1;
    public bool followTarget = true;
    public bool canSlowEnemies = false;
    public float freezeTowerSlowdown = 0;
    public bool hasExplosion = false;
    public float explosionDamage_subtractfrombulletdamage = -10f;
    public GameObject exposionPrefab;
    public float explosionRadius = 5;
    public bool hasParticles = false;
    public GameObject particlePrefab;
    public bool stopMoving;
    public bool isTNT = false;
    public bool otherbullets = true;
    public float freezeDuration = 0;
    public float realTNTFuse;
}
