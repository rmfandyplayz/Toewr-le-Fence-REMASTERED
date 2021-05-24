using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class upgradebutton : MonoBehaviour
{
    public float upgradeprice = 200f;
    public float moneymultiplier = 1.5f;
    public Button multiplybutton;
    public money moneyscript;
    public float capacityMultiplier = 2f;
    public float cooldownSubtractor = 0.025f;
    public float moneyIncrease = 1f;
    public TextMeshProUGUI upgradetext;

    public float stonksLevel = 1;
    public TextMeshProUGUI stonksLevelText;

    void Start()
    {
        multiplybutton.interactable = false;
        upgradetext.text = $"${upgradeprice}";
        stonksLevelText.text = $"LVL {stonksLevel}";
    }

    public void upgrade()
    {
        
        moneyscript.maxCapacity *= capacityMultiplier;
        if (moneyscript.moneyCooldown > 0)
        {
            moneyscript.moneyCooldown -= cooldownSubtractor;
            moneyscript.moneyCooldown = Mathf.Clamp(moneyscript.moneyCooldown, 0, Mathf.Infinity);
        }
        else
        {
            moneyscript.moneyGiven += moneyIncrease;
        }

        stonksLevel += 1;
        moneyscript.spendMoney(upgradeprice);
        upgradeprice *= 2f;
        multiplybutton.interactable = false;
        upgradetext.text = $"${upgradeprice}";
        stonksLevelText.text = $"LVL {stonksLevel}";
        //StopAllCoroutines();
    }

    void Update()
    {

        if (!multiplybutton.interactable && moneyscript.currentMoney >= upgradeprice)
        {
            multiplybutton.interactable = true;

        }

        if(multiplybutton.interactable && moneyscript.currentMoney < upgradeprice)
        {
            multiplybutton.interactable = false;
        }
    }
    /*
    IEnumerator buttonFlash(int numberofflashes)
    {
        Debug.LogWarning("Startflash");
        //Image color = multiplybutton.GetComponent<Image>();
        Color offcolor = new Color(115, 115, 115, 255);
        Color oncolor = new Color(255, 255, 255, 255);
        for (int i = 0; i < numberofflashes; i++)
        {
            multiplybutton.colors = generatecolorblock(multiplybutton.colors, offcolor);
            Debug.LogWarning("Off");
            yield return new WaitForSeconds(0.3333333f);
            multiplybutton.colors = generatecolorblock(multiplybutton.colors, oncolor);
            Debug.LogWarning("On");
            yield return new WaitForSeconds(0.3333333f);
        }
    }

    ColorBlock generatecolorblock(ColorBlock original, Color newColor)
    {
        ColorBlock colorBlock = original;
        colorBlock.normalColor = newColor;        return colorBlock;

    }
    */

}
