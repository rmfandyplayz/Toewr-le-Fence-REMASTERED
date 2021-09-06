using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Object/Turret", order = 1)]
public class TurretSettings : ScriptableObject
{
    public string turretName;
    public Sprite turretSprite;
    public bool canRotate = true;
    [ShowIf(nameof(canRotate), true)]
    public UpgradableType rotateSpeed = new UpgradableType(5);
    public UpgradableType range;
    public UpgradableType fireRate;
    public int buyPrice;
    public Vector2 rangeVizPosition;
    public Obj2DSimpleInfo colliderPositionAndSize;
    public List<Obj2DSimpleInfo> firepointPositionRotation;

    public BulletSetup bullet;

    public List<TypeOfUpgrade> upgrades = new List<TypeOfUpgrade>();
}


[System.Serializable]
public struct Obj2DSimpleInfo
{
    public Vector2 position;
    public float value;
}

public enum TypeOfUpgrade
{
    Rotation, Range, FireRate, BulletDamage
}