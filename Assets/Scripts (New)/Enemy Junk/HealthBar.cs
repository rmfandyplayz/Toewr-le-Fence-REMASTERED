using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platinio.TweenEngine;

public class HealthBar : MonoBehaviour
{
    public float maxValue = 100;
    public float healthDropAnimationTimer = 1;
    private int timesCalled = 0;
    public Image hpBar;
    public Ease ease;


    [SerializeField]
    public bool startingAtMax = true;

    void Awake()
    {
        PlatinioTween.instance.FillAmountTween(hpBar, 1, 0).SetEase(ease).SetOwner(gameObject);
        //StartCoroutine(ShowBars(0.06f, true));
        foreach (Image i in GetComponentsInChildren<Image>())
        {
            i.FadeOut(0);
        }
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
            //i.enabled = toggle;
            if(toggle == true)
            {
                i.FadeIn(0.2f);
            }
            else
            {
                i.FadeOut(0.2f);
            }
        }
    }
    public IEnumerator ShowBars(float timer = 0.5f, bool isHighPriority = false)
    {
        //Debug.Log("ShowBars() ran");
        ToggleBars(true);
        timesCalled++;
        yield return new WaitForSeconds(timer);
        timesCalled--;
        if (isHighPriority == true || timesCalled <= 0)
        {
            ToggleBars(false);
            timesCalled = 0;
            StopAllCoroutines();
        }
        //ToggleBars(false);
    }
}
