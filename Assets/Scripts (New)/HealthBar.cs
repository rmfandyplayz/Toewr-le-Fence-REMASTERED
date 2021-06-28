using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platinio.TweenEngine;

public class HealthBar : MonoBehaviour
{
    public float maxValue = 100;
    public float healthDropAnimationTimer = 1;
    public Image hpBar;
    public Ease ease;
    //public Slider sliderBar;

    [SerializeField]
    public bool startingAtMax = true;

    void Awake()
    {
        //sliderBar.maxValue = maxValue;
        /*
        Debug.Log(hpBar);
        if (startingAtMax)
        {
            //sliderBar.value = maxValue;
            hpBar.fillAmount = maxValue / maxValue;
        }
        else
        {
            //sliderBar.value = 0;
            hpBar.fillAmount = 0;
        }
        Debug.Log(hpBar);
        */
    }

    public void SetMaxValue(float newValue)
    {
        maxValue = newValue;
        //sliderBar.maxValue = newValue;
        /*
        Debug.Log(hpBar);
        hpBar.fillAmount = maxValue / maxValue;
        Debug.Log(hpBar);
        */
    }

   public void UpdateValue(float newValue)
   {
        Debug.Log("Float newvalue of UpdateValue ran");
        gameObject.CancelAllTweens();
        //sliderBar.value = newValue;
        Debug.Log(hpBar);
        PlatinioTween.instance.FillAmountTween(hpBar, newValue / maxValue, healthDropAnimationTimer).SetEase(ease).SetOwner(gameObject);
    }
   public void UpdateValue(int newValue)
   {
        Debug.Log("Int newvalue of UpdateValue ran");
        gameObject.CancelAllTweens();
        //sliderBar.value = newValue;
        Debug.Log(hpBar);
        PlatinioTween.instance.FillAmountTween(hpBar, (float)newValue / maxValue, healthDropAnimationTimer).SetEase(ease).SetOwner(gameObject);
    }
}
