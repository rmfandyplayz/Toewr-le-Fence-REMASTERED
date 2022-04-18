using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "Status Effect")]
public class StatusEffectsScriptObj : ScriptableObject
{
    [Header("General")]
    public string statusName;
    public bool isAnimatedActiveIcon = false;
    [ShowIf(nameof(isAnimatedActiveIcon), true)] public AtlasAnimator animatedActiveIcon;
    [ShowIf(nameof(isAnimatedActiveIcon), false)] public Image activeIcon;
    public Image immuneIcon;
    public float effectDuration;
    public bool canBeStacked;

    [Header("Custom Functionality")]
    public ScriptGraphAsset customFunctionality_Visual;
    public Component customFunctionality_Script;

}
