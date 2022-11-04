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
    public bool isUpdating = false;
    [SerializeField] RunTween motionTween;
    [SerializeField] RunTween colorTween;
    Vector3 indicatorDirection;

    void OnEnable()
    {
        //Debug.Log(this.transform.position);
        StartCoroutine(IndicatorMovement());
        motionTween.RunTweenWithDynamicVector3(indicatorDirection);
        if (colorTween != null)
        {
            colorTween.RunTweenDefault();
        }
        damageIndicatorText.alpha = 1;
        //Debug.Log("Updating (DamageIndicator.cs)");
    }
    private void OnDisable()
    {
        isUpdating = false;
    }

    /// <summary>
    /// Initializes the damage indicators for rendering. Does not actually render the damage indicators.
    /// This function basically tells the text what damage number to display.
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="indicatorType"></param>
    /// <param name="spawnPos"></param>
    public void InitializeIndicator(float damage, damageIndicatorType indicatorType, Vector3 spawnPos)
    {
        isUpdating = true;
        damageIndicatorText = GetComponent<TMP_Text>();
        damageType = indicatorType;
        damageIndicatorText.text = $"{damage}";
        this.transform.position = spawnPos;
        //Debug.Log(this.transform.position);
        
    }

    public IEnumerator IndicatorMovement()
    {
        yield return new WaitForSeconds(2.5f);
        ObjectPooling.ReturnObject(this.gameObject);
    }
}