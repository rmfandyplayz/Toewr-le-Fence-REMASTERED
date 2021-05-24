using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonScript : MonoBehaviour
{
    [SerializeField]
    private UpgradeType buttonType;
    public money moneyScript;
    private bool isMaxed = false;
    
    public void Innit(UpgradeType newtype)
    {
        buttonType = newtype;
        isMaxed = !CheckUpgradeAmount(buttonType.startingAmount + buttonType.upgradeEfficiency * (buttonType.Counter + 1), buttonType.maxUpgradeValue, buttonType.isMaximum);
        if (isMaxed == true)
        {
            UpdateCost(true);
        }


        GetComponent<Button>().onClick.AddListener(() => {
            float newValue = buttonType.startingAmount + buttonType.upgradeEfficiency * (buttonType.Counter +1 );
            float cost = buttonType.Cost + buttonType.Counter * buttonType.costIncreaseAmount;
            Debug.LogWarning($"{buttonType.Name},{newValue},{cost}");
            if (CheckUpgradeAmount(newValue,buttonType.maxUpgradeValue,buttonType.isMaximum) && CheckMoneyAmount(moneyScript.currentMoney, cost))
            {
                buttonType.ClickedButton(newValue);
                moneyScript.spendMoney(cost);
                buttonType.Counter++;
                UpdateCost();
            }
            else if (!CheckUpgradeAmount(newValue, buttonType.maxUpgradeValue, buttonType.isMaximum))
            {   
                UpdateCost(true);
                isMaxed = true;
            }
            else
            {
                GetComponent<Button>().interactable = false;

            }
        });
        
        //GetComponentInChildren<Text>().text = $"{buttonType.Name}\n${buttonType.Cost + (buttonType.Counter * buttonType.costIncreaseAmount)}";
        //GetComponent<Button>().onClick.AddListener(() => UpdateCost());
    }

    public void UpdateCost(bool overWriteCost = false)
    {
        if (overWriteCost == true || !CheckUpgradeAmount(buttonType.startingAmount + buttonType.upgradeEfficiency * buttonType.Counter , buttonType.maxUpgradeValue, buttonType.isMaximum))
        {
            GetComponentInChildren<Text>().text = $"{buttonType.Name}\nMAXED OUT!";

        }
        else
        {
            GetComponentInChildren<Text>().text = $"{buttonType.Name}\n${buttonType.Cost + (buttonType.Counter * buttonType.costIncreaseAmount)}";
        }
    }

    public bool CheckMoneyAmount(float currentMoney, float upgradePrice)
    {
        return currentMoney >= upgradePrice;

    }



    public bool CheckUpgradeAmount(float amount, float limiter, bool isMaximum)
    {
        if (isMaximum == true)
        {
            return amount < limiter;
        }
        else
        {
            return amount > limiter;
        }
    }

    void Update()
    {

        if (isMaxed == true || !CheckMoneyAmount(moneyScript.currentMoney , buttonType.Cost + buttonType.Counter * buttonType.costIncreaseAmount) || (!CheckUpgradeAmount(buttonType.startingAmount + buttonType.upgradeEfficiency * buttonType.Counter, buttonType.maxUpgradeValue, buttonType.isMaximum)))
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;

        }
    }



}
