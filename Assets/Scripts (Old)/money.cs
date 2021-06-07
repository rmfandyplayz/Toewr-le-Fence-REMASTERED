using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class money : MonoBehaviour {
    public float currentMoney = 0;
    Text currencyText;
    public float moneyCooldown = 1f;
    public float moneyGiven = 1;
    public float currentCooldown = 0f;
    public int KeepTrackLevels = 1;
    public float maxCapacity = 200;
    public bool isMoneyActive = false;
	// Use this for initialization
	void Start () {
        currencyText = GetComponent<Text>();
        currencyText.text = $"${formatting(currentMoney)}/{formatting(maxCapacity)}";
        isMoneyActive = false;
        StartCoroutine(UpdateMoney());
	}

    IEnumerator UpdateMoney()
    {
        yield return new WaitForSeconds(0.01f); 
        currencyText.text = $"${formatting(currentMoney)}/{formatting(maxCapacity)}";

    }
	
	// Update is called once per frame
	void Update () {
        if(isMoneyActive == true)
        {
            Debug.LogWarning(isMoneyActive);
            currentCooldown += Time.deltaTime;
            if (currentCooldown > moneyCooldown)
            {
                giveMoney();
                currentCooldown = 0f;

            }
        }
	}
    
    public void giveMoney() {
        if (currentMoney < maxCapacity)
        {
            currentMoney += moneyGiven;
            currentMoney = Mathf.Clamp(currentMoney, 0, maxCapacity);
            currencyText.text = $"${formatting(currentMoney)}/{formatting(maxCapacity)}";
        }
    }
    public void giveMoney(float Amount)
    {
        if (currentMoney < maxCapacity)
        {
            currentMoney += Amount;
            currentMoney = Mathf.Clamp(currentMoney, 0, maxCapacity);
            currencyText.text = $"${formatting(currentMoney)}/{formatting(maxCapacity)}";
        }
    }
    public bool spendMoney(float amount,bool test = false) {
        if (amount <= currentMoney) {
            if(!test)
                currentMoney -= amount;
                currencyText.text = $"${formatting(currentMoney)}/{formatting(maxCapacity)}";
            return true;
        }
        return false;
    }
    
    /*
    public void DecreaseCooldown()
    {
        moneyCooldown -= .01f;
        moneyGiven -= .0025f;
        currentMoney -= 150f;
        

    }
    public void IncreaseMoneyIncome()
    {
        moneyGiven += .1f;
        moneyCooldown += .005f;
        currentMoney -= 150f;
    }
    */
    public string formatting(float money)
    {
        if (money >= 1000000000)
        {
            return $"{money / 1000000000:0}B";
        }
        else if (money >= 100000000)
        {
            return $"{money / 100000000:0}M";
        }
        else if (money >= 1000000)
        {
            return $"{money / 1000000}M";
        }
        else if (money >= 100000)
        {
            return $"{money / 1000:0}k";
        }
        else if (money >= 1000)
        {
            return $"{money / 1000:0.0}k";
        }
        else
        {
            return $"{money}";
        }
    }
}
