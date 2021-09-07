using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DamageIndicator : MonoBehaviour
{
    public TMP_Text damageIndicatorText;
    public damageIndicatorType damageType;
    public float dissappearTimer;
    private float angle = 0;
    public float indicatorSpeed = 100;

    public void InitializeIndicator(float damage, damageIndicatorType indicatorType)
    {
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
    void Start()
    {
        angle = Random.Range(0, 360);

        StartCoroutine(IndicatorMovement(angle));
        //Debug.Log("Updating (DamageIndicator.cs)");
    }

    void Update()
    {
        Vector3 target = transform.position + new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad), 0).normalized;
        transform.position = Vector2.MoveTowards(this.transform.position, target, indicatorSpeed * Time.deltaTime);
    }

    // public void RunIndicator(float damage, Vector3 position)
    // {
    //     GameObject popup = Instantiate(this.gameObject, position, Quaternion.identity, GameObject.FindGameObjectWithTag("World Canvas").transform);
    //     popup.GetComponent<DamageIndicator>().damageIndicatorText.text = damage.ToString();
    //     //Debug.Log("StartCoroutine() ran (DamageIndicator.cs)");
    // }

    public IEnumerator IndicatorMovement(float angle)
    {
        for(int i = 0; i < 10; i++)
        {
            damageIndicatorText.alpha -= 0.1f;
            yield return new WaitForSeconds(dissappearTimer/10);
        }
        Destroy(this.gameObject, + 0.01f);
    }
}