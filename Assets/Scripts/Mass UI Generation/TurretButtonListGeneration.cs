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
    [SerializeField] private List<GameObject> generatedButtons = new List<GameObject>();
    

    [EditorButton(nameof(GenerateButtons))] public bool test; //TEMPORARY
    //Functions section

    public void GenerateButtons(GameObject parentTab)
    {
        foreach(var tab in AddressablesHolder.FilterByType(typeof(TurretSettings)))
        {
            if(tab is TurretSettings turret)
            {
                Debug.LogWarning(turret.name);
                var newButtons = Instantiate(turretButtons);
                newButtons.transform.SetParent(parentTab.transform);
                newButtons.GetComponent<TurretButtonInitializing>().InitializeButton(turret);

            }
        }
    }

    public void GenerateTabs()
    {
        //First, intiallize all the tabs, and then generate the buttons
        
        //After the generation completes, filter each turret by the Tab they belong to
        
        foreach (var tab in AddressablesHolder.FilterByType(typeof(TabScriptableObject)))
        {
            if (tab is TabScriptableObject tabScriptableObject)
            {
                var newTab = Instantiate(turretTabs);
                newTab.transform.SetParent(parentObject.transform);
                newTab.GetComponent<TabInitializer>().InitializeTab(tabScriptableObject);
                GenerateButtons(newTab);
            }
        }
    }
}
