using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using log4net.DateFormatter;
using UnityEditor.Experimental.GraphView;

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
        StartCoroutine(IndicatorMovement());
        motionTween.RunTweenWithDynamicVector3(indicatorDirection);
        if (colorTween != null)
        {
            colorTween.RunTweenDefault();
        }
        damageIndicatorText.alpha = 1;
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
    public void InitializeIndicator(float damage, damageIndicatorType indicatorType, Vector3 spawnPos, Vector3 velocity)
    {
        isUpdating = true;
        damageIndicatorText = GetComponent<TMP_Text>();
        damageType = indicatorType;
        damageIndicatorText.text = $"{damage}";

        //If the enemy is going left or right
        if(Mathf.Abs(velocity.x) > 0)
        {
            indicatorDirection = new Vector3(-MathF.Sign(velocity.x), 1, 1);
        }
        //If the enemy is going up or down
        else
        {
            indicatorDirection = new Vector3((Camera.main.pixelWidth / 2) - this.transform.position.x >= 0 ? 1 : -1, 1, 1);
        }
        Debug.LogError($"IndicatorDirection: {indicatorDirection}");
        this.transform.position = spawnPos;
        if(indicatorDirection.x < 0)
        {
            damageIndicatorText.rectTransform.pivot = new Vector2(0, damageIndicatorText.rectTransform.pivot.y);
        }
    }

    public IEnumerator IndicatorMovement()
    {
        yield return new WaitForSeconds(2.5f);
        ObjectPooling.ReturnObject(this.gameObject);
    }
}