using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretButtonListGeneration : MonoBehaviour
{
    //Variables section
    [SerializeField] private GameObject turretButtons;
    [SerializeField] private GameObject turretTabs;
    [SerializeField] private List<string> tabs = new List<string>();
    [SerializeField] private GameObject parentObject;
    

    [EditorButton(nameof(GenerateButtons))] public bool test; //TEMPORARY
    //Functions section

     public void GenerateButtons()
    {
        foreach(var tab in AddressablesHolder.FilterByType(typeof(TurretSettings)))
        {
            if(tab is TurretSettings turret)
            {
                Debug.LogWarning(turret.name);
                var newButtons = Instantiate(turretButtons);
                newButtons.transform.SetParent(parentObject.transform);
                newButtons.GetComponent<TurretButtonInitializing>().InitializeButton(turret);

            }
        }
    }

    
}
