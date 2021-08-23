using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Object/Upgrade")]
public class TurretUpgradeSetup : ScriptableObject
{
    public int initialPrice = 1;
    public float priceIncreaseMultiplier = 1;
    public int priceIncreaseConstant = 0;

    public List<UpgradeHolder> upgrader;
}

public interface IUpgradable
{}

[System.Serializable]
public class UpgradeHolder
{
    public IUpgradable script;
    public int x;
}