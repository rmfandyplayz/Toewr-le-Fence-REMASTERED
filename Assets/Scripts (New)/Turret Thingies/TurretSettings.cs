using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Object/Turret", order = 1)]
public class TurretSettings : ScriptableObject
{
    public string turretName;
    public Sprite turretSprite;
    public int buyPrice;
    public bool canRotate = true;
    public BulletSetup bullet;
    public Vector2 rangeVisualizationPosition;
    [ShowIf(nameof(canRotate), true)]
    [HideInInspector] public UpgradableType rotateSpeed = new UpgradableType(5);
    public UpgradableType range;
    public UpgradableType fireRate;
    public Obj2DSimpleInfo colliderPositionAndSize;
    public List<Obj2DSimpleInfo> firepointPositionRotation;
    public List<TypeOfUpgrade> upgrades = new List<TypeOfUpgrade>();
    [ClassExtends(typeof(TurretAttackType))] public SerializedType turretType;

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