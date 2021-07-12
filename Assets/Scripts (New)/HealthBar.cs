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


    [SerializeField]
    public bool startingAtMax = true;

    void Awake()
    {
        ToggleBars(false);
        PlatinioTween.instance.FillAmountTween(hpBar, 1, healthDropAnimationTimer).SetEase(ease).SetOwner(gameObject);
    }

    public void SetMaxValue(float newValue)
    {
        maxValue = newValue;
    }

   public void UpdateValue(float newValue)
   {
        if (hpBar == null)
        {
            GetHPImage();
        }
        gameObject.CancelAllTweens();
        RunTween(newValue / maxValue);
    }
   public void UpdateValue(int newValue)
   {
        if (hpBar == null)
        {
            GetHPImage();
        }
        gameObject.CancelAllTweens();
        RunTween(newValue / maxValue);
    }
    public void GetHPImage()
    {
        hpBar = GetComponentInChildren<Image>();
    }
    public void RunTween(float value)
    {
        if (hpBar == null)
        {
            GetHPImage();
        }
        StartCoroutine(ShowBars());
        PlatinioTween.instance.FillAmountTween(hpBar, value, healthDropAnimationTimer).SetEase(ease).SetOwner(gameObject);
    }
    public void ToggleBars(bool toggle = false)
    {
        //Debug.Log("ToggleBars() ran");
        GetComponent<Image>().enabled = toggle;
        foreach (Image i in GetComponentsInChildren<Image>())
        {
            i.enabled = toggle;
        }
    }
    public IEnumerator ShowBars(float timer = 2)
    {
        //Debug.Log("ShowBars() ran");
        ToggleBars(true);
        yield return new WaitForSeconds(timer);
        ToggleBars(false);
    }
}
