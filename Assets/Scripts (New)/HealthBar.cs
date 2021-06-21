using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float maxValue = 100;
    public Slider sliderBar;

    [SerializeField]
    public bool startingAtMax = true;

    void Awake()
    {
        sliderBar.maxValue = maxValue;
        if(startingAtMax)
        {
            sliderBar.value = maxValue;
        }
        else
        {
            sliderBar.value = 0;
        }
    }

    public void SetMaxValue(float newValue)
    {
        maxValue = newValue;
        sliderBar.maxValue = newValue;
    }

   public void UpdateValue(float newValue)
   {
       sliderBar.value = newValue;
   }
   public void UpdateValue(int newValue)
   {
       sliderBar.value = newValue;
   }
}
