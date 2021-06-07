using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Object/Turret", order = 1)]
public class TurretSettings : ScriptableObject
{
    public string turretName;
    public Sprite turretSprite;
    public bool canRotate = true;
    public int rotateSpeed = 5;
    public float range;
    public float fireRate;
    public int buyPrice;
    public BulletSetup bullet;
    public List<TurretUpgradeSetup> tupgrades; 
    /*
    What to put here:
    Bullet scriptable object
    Complete this one
    Upgrades scriptable object

    What to draw:
    Health and shield bar
    GUI HUD
    Buy turrets scrollview stuff
    Upgrade turrets scrollview stuff
    Buttons


    */


}
