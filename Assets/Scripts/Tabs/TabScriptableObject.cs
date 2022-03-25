using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;

[CreateAssetMenu(menuName = "Turret Tab")]
public class TabScriptableObject : ScriptableObject
{
    public string tabName;
    public Sprite tabBackgroundImage;
    [NewLabel("Is the name too large for the tab?")] public bool expands;
    [ShowIf(nameof(expands), true), NewLabel("Chunks for background image expansion")] public Sprite tabBackgroundImageExpansionChunk;

    public List<TurretSettings> turrets = new List<TurretSettings>();
    //No longer need have turrets go under parent object, straight up under the tab
}
