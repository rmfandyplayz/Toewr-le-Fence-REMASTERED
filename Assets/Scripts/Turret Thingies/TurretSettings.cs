using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Object/Turret", order = 1)]
public class TurretSettings : ScriptableObject
{
    public string turretName;
    public Sprite turretSprite;
    public int buyPrice;
    public UpgradableType range;
    public UpgradableType fireRate;
    public float deploymentCooldown; // This variable may be changed in the future.
    public bool canRotate = true;
    public Vector2 rangeVisualizationPosition;
    [ShowIf(nameof(canRotate), true)]
    [HideInInspector] public UpgradableType rotateSpeed = new UpgradableType(5);
    
    [HideInInspector] public Obj2DSimpleInfo colliderPositionAndSize;
    public List<Obj2DSimpleInfo> firepointPositionRotation;
    public List<TypeOfUpgrade> upgrades = new List<TypeOfUpgrade>();
    [ClassExtends(typeof(TurretAttackType))] public SerializedType turretType;

    public bool attackTypeIsMeele => turretType.Type == typeof(Melee);
    [Header("Meele Attack Type Settings")]
    [ShowIf(nameof(attackTypeIsMeele), true)] public GameObject meeleTurretPrefab;
    [ShowIf(nameof(attackTypeIsMeele), true)] public AnimationClip idleAnimation;
    [ShowIf(nameof(attackTypeIsMeele), true)] public AnimationClip attackAnimation;


    [InLineEditor] public BulletSetup bulletSetup;
}


[System.Serializable]
public struct Obj2DSimpleInfo
{
    public Vector2 position;
    public float value;
}

public enum TypeOfUpgrade
{
    Rotation, Range, FireRate, BulletDamage, DankChance, SurrealChance, NoScopeChance
}