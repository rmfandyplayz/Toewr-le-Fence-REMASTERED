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

     public void GenerateButtons()
    {
        foreach(var tab in AddressablesHolder.FilterByType(typeof(TurretSettings)))
        {
            if(tab is TurretSettings turret)
            {
                Debug.LogWarning(turret.name);
                // Instantiate a new game object from the turret button
                // Get the turret button initializing
                // Pass in turret to the initializing function

                var newButtons = Instantiate(turretButtons);
                newButtons.GetComponent<TurretButtonInitializing>().InitializeButton(turret);

            }
        }
    }

    
}
