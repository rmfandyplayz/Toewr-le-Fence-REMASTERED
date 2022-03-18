using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretButtonListGeneration : MonoBehaviour
{
    //Variables section
    [SerializeField] private GameObject turretButtons;
    [SerializeField] private GameObject turretTabs;
    [SerializeField] private List<string> tabs = new List<string>();

    [EditorButton(nameof(GenerateButtons))] public bool test; //TEMPORARY

    //Functions section
    // Generate Buttons function
    // 1. Get all of the turret setting from the addressable holder
    // 2. Generate a button using each of the turret setting from the previous step ()

     public void GenerateButtons()
    {
        foreach(var tab in AddressablesHolder.FilterByType(typeof(TurretSettings)))
        {
            if(tab is TurretSettings turret)
            {
                Debug.LogWarning(turret.name);
            }
        }
    }

    
}
