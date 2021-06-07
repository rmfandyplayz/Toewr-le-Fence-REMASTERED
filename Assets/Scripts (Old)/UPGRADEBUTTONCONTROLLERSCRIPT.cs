using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UPGRADEBUTTONCONTROLLERSCRIPT : MonoBehaviour
{
    public GameObject UpgradePanel;
    public Camera camera;
    private bool isActive;
    public List<GameObject> buttons;
    private AllTurretsUpgrade allturretsupgradetemp;
    public DestroyTurret deleteButton;


    private void Start()
    {
        isActive = false;
        UpgradePanel.SetActive(isActive);
        camera = Camera.main;
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
            button.GetComponent<Button>().onClick.RemoveAllListeners();
        }

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && isActive == true)
        {
            Debug.LogWarning("It's running");
            isActive = false;

            foreach (GameObject button in buttons)
            {
                button.SetActive(false);
                button.GetComponent<Button>().onClick.RemoveAllListeners();
            }
            UpgradePanel.SetActive(isActive);
            allturretsupgradetemp?.circlerange.ToggleRangeVisual(isActive);
            allturretsupgradetemp = null;
        }

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);





            if (Physics.Raycast(ray, out hit))
            {
                AllTurretsUpgrade tower = hit.collider.gameObject.GetComponent<AllTurretsUpgrade>();
                //Debug.LogWarning(allturretsupgradetemp);
                if (tower != null)
                {
                    //Debug.LogWarning("✔ New Tower Selected");
                    isActive = !isActive;
                    

                    tower.ButtonInfo(buttons);
                    UpgradePanel.SetActive(isActive);
                    tower.circlerange.ToggleRangeVisual(isActive);
                    
                    

                    if (allturretsupgradetemp != null && allturretsupgradetemp != tower)
                    {

                        allturretsupgradetemp?.circlerange.ToggleRangeVisual(false);
                    }
                    //allturretsupgradetemp != null


                    allturretsupgradetemp = tower;
                    deleteButton.selectedTurret = tower.GetComponent<AllTurrets>();
                   
                }
                /*
                else
                {
                    
                    isActive = false;
                    
                    foreach (GameObject button in buttons)
                    {
                        button.SetActive(false);
                        button.GetComponent<Button>().onClick.RemoveAllListeners();
                    }
                    UpgradePanel.SetActive(isActive);
                    allturretsupgradetemp.circlerange.ToggleRangeVisual(isActive);
                    allturretsupgradetemp = null;
                }
                */
            }
        }
    }













}
