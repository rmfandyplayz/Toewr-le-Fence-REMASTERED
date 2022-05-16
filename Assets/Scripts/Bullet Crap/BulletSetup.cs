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
    [Header("Bools")]
    public bool followTarget = true;
    public bool hasParticles = false;
    public bool stopMoving;
    public bool explodes = false;
    [BeginIndent]
    [ShowIf(nameof(explodes), true)] public float explosionRadius;
    [EndIndent]
    [ShowIf(nameof(explodes), true)] public float explosionDamagePercent = 50;
    public float ExplosionRadius => explodes ? explosionRadius : 0;
    public List<StatusEffectsInfoCarry> statusEffects;

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

    public damageIndicatorType DamageCalculation(ref float damage, SerializedDictionary<TypeOfUpgrade, int> upgradeCounter)
    {
        int RNG = Random.Range(0, 100);
        var noScopeDmgPerc = this.NoscopeDmgChance.GetUpgradedValue(upgradeCounter.CounterValue(TypeOfUpgrade.NoScopeChance));
        var surrealDmgPerc = this.surrealDmgChance.GetUpgradedValue(upgradeCounter.CounterValue(TypeOfUpgrade.SurrealChance));
        var dankDmgPerc = this.dankDmgChance.GetUpgradedValue(upgradeCounter.CounterValue(TypeOfUpgrade.DankChance));


        if (noScopeDmgPerc > RNG)
        {
            damage = this.noscopeDmg;
            return damageIndicatorType.mlgNoScope;
        }
        else if (surrealDmgPerc > RNG - noScopeDmgPerc)
        {
            damage *= this.surrealDmgMultiplier;
            return damageIndicatorType.surrealDamage;
        }
        else if (dankDmgPerc > RNG - noScopeDmgPerc - surrealDmgPerc)
        {
            damage *= this.dankDmgMultiplier;
            return damageIndicatorType.dankDamage;
        }
        else
        {
            return damageIndicatorType.normieDamage;
        }
    }

}

[System.Serializable]
public class StatusEffectsInfoCarry
{
    [InLineEditor] public StatusEffectsScriptObj statusEffect;
    [HideIf(nameof(statusEffect), false)] public float chance;
    [HideIf(nameof(statusEffect), false)] public float duration;
    [HideIf(nameof(statusEffect), false)] public int tier; //Max 10 tiers
    
    
}