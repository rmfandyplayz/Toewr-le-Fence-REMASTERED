using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public TMP_Text damageIndicatorText;
    public damageIndicatorType damageType;
    public float dissappearTimer;
    public float indicatorSpeed = 100;
    public bool isUpdating = false;
    [SerializeField] RunTween motionTween;
    [SerializeField] RunTween colorTween;
    Vector3 indicatorDirection;

    void OnEnable()
    {
        StartCoroutine(IndicatorMovement());
        motionTween.RunTweenWithDynamicVector3(indicatorDirection);
        colorTween.RunTweenDefault();
        damageIndicatorText.alpha = 1;
        //Debug.Log("Updating (DamageIndicator.cs)");
    }
    private void OnDisable()
    {
        isUpdating = false;
    }


    public void InitializeIndicator(float damage, damageIndicatorType indicatorType)
    {
        isUpdating = true;
        damageIndicatorText = GetComponent<TMP_Text>();
        damageType = indicatorType;
        if(damageType == damageIndicatorType.mlgNoScope)
        {
            damageIndicatorText.text = "360° Noscope'd";
        }
        else
        {
            damageIndicatorText.text = $"{damage}";
        }
    }
    

    void Update()
    {
        if (isUpdating)
        {
            
        }
    }

    public IEnumerator IndicatorMovement()
    {
        for(int i = 0; i < 10; i++)
        {
            //damageIndicatorText.alpha -= 0.1f;
            yield return new WaitForSeconds(dissappearTimer/10);
        }
        ObjectPooling.ReturnObject(this.gameObject);
    }
}