using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;

[CreateAssetMenu(menuName = "Turret Tab")]
public class TabScriptableObject : ScriptableObject
{
    public string tabName;
    [NewLabel("Does the button use a custom background?")] public bool useCustomBackground;
    [ShowIf(nameof(useCustomBackground), true)] public Sprite customBackground;
    
    public Sprite tabBackgroundImage;
    [NewLabel("Is the name too large for the tab?")] public bool expands;
    [ShowIf(nameof(expands), true), NewLabel("Chunks for background image expansion")] public Sprite tabBackgroundImageExpansionChunk;
}
