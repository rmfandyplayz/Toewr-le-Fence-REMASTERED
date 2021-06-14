using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Object/Bullet")]
public class BulletSetup : ScriptableObject
{
    [Header("Sprites and Idk")]
    public Sprite bulletSprite;
    public GameObject particlePrefab;
    public GameObject exposionPrefab;
    [Header("Floats and Values")]
    public float speed = 70f;
    public float bulletDamage = 1;
    public float freezeTowerSlowdown = 0;
    public float explosionDamage_subtractfrombulletdamage = -10f;
    public float explosionRadius = 5;
    public float freezeDuration = 0;
    public float realTNTFuse;
    [Header("Bools")]
    public bool followTarget = true;
    public bool canSlowEnemies = false;
    public bool hasParticles = false;
    public bool stopMoving;
    public bool isTNT = false;
    public bool otherbullets = true;
    public bool hasExplosion = false;
}
