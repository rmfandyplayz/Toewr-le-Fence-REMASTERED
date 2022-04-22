using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using Toolbox;

[CreateAssetMenu(menuName = "Status Effect")]
public class StatusEffectsScriptObj : ScriptableObject
{
    [Header("General")]
    public string statusName;
    public bool isAnimatedActiveIcon = false;
    [ShowIf(nameof(isAnimatedActiveIcon), true)] public AtlasAnimator animatedActiveIcon;
    [ShowIf(nameof(isAnimatedActiveIcon), false)] public Sprite activeIcon;
    public Sprite immuneIcon;
    public float effectDuration;
    public bool canBeStacked;

    [Header("Custom Functionality")]
    public ScriptGraphAsset customFunctionality_Visual;
    [ClassExtends(typeof(StatusEffectsCustomFunctionality))] public UnityEngine.SerializedType customFunctionality_Script;
    [TextArea(minLines: 8, maxLines: 100), Disable] public string note = @"
    To add custom functionality (for a normal C# script, not visual script):
    • Create a new class, name it anything you want
    • Inherit from StatusEffectsCustomFunctionality
    • Override run functionalitites.
    ";
}

/*
To add custom functionality:
• Create a new class, name it anything you want
• Inherit from StatusEffectsCustomFunctionality
• Override run functionalitites.
*/

public class TestingFunctionality : StatusEffectsCustomFunctionality
{
    
}
