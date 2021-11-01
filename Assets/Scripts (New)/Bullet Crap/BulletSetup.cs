using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;

[CreateAssetMenu(fileName = "Bullet", menuName = "Object/Bullet")]
public class BulletSetup : ScriptableObject
{
    [Header("Sprites and Idk")]
    public Sprite bulletSprite;
    public GameObject particlePrefab;
    public GameObject exposionPrefab;
    [Header("Floats and Values")]
    public float speed = 70f;
    public UpgradableType bulletDamage;
    public float freezeTowerSlowdown = 0;
    public float freezeDuration = 0;
    public float realTNTFuse;
    [Header("Bools")]
    public bool followTarget = true;
    public bool canSlowEnemies = false;
    public bool hasParticles = false;
    public bool stopMoving;
    public bool isTNT = false;
    public bool otherbullets = true;
    public bool explodes = false;
    [BeginIndent]
    [ShowIf(nameof(explodes), true)] public float explosionRadius;
    [EndIndent]
    [ShowIf(nameof(explodes), true)] public float explosionDamagePercent = 50;
    public float ExplosionRadius => explodes ? explosionRadius : 0;


    [Header("Upgrades")]
    public List<TypeOfUpgrade> upgrades = new List<TypeOfUpgrade>();
    [Header("Damage Type and Chance")]

    public bool canDealDankDmg;
    [ShowIf(nameof(canDealDankDmg), true)]
    [SerializeField] UpgradableType _dankDmgChance = new UpgradableType(0);
    public UpgradableType dankDmgChance => canDealDankDmg? _dankDmgChance: new UpgradableType(0);
    [ShowIf(nameof(canDealDankDmg), true)]
    public float dankDmgMultiplier;

    public bool canDealSurrealDmg;
    [ShowIf(nameof(canDealSurrealDmg), true)]
    [SerializeField] UpgradableType _surrealDmgChance = new UpgradableType(0);
    public UpgradableType surrealDmgChance => canDealSurrealDmg? _surrealDmgChance: new UpgradableType(0);
    [ShowIf(nameof(canDealSurrealDmg), true)]
    public float surrealDmgMultiplier;

    public bool canDealNoscopeDmg;
    [ShowIf(nameof(canDealNoscopeDmg), true)]
    [SerializeField] UpgradableType _noscopeDmgChance = new UpgradableType(0);
    public UpgradableType NoscopeDmgChance => canDealNoscopeDmg? _noscopeDmgChance: new UpgradableType(0);
    [ShowIf(nameof(canDealNoscopeDmg), true)]
    public float noscopeDmg = Mathf.Infinity;
    
}
