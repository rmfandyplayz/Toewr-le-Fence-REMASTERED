using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTurret : MonoBehaviour
{
    public AllTurrets selectedTurret;
    public money moneyScript;
    public GameObject upgradePanel;

    public void Remove()
    {
            Debug.LogWarning(selectedTurret);
        if (selectedTurret != null) {

            
            moneyScript.giveMoney(Mathf.Round(selectedTurret.buyPrice/2));
            Destroy(selectedTurret.gameObject);
            upgradePanel.SetActive(false);
            selectedTurret = null;
            
        }
    }







}
