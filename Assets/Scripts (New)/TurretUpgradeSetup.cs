using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Object/Upgrade")]
public class TurretUpgradeSetup : ScriptableObject
{
    public int initialPrice = 1;
    public float priceIncreaseMultiplier = 1;
    public int priceIncreaseConstant = 0;

    // TODO: Specify which function to call

}
