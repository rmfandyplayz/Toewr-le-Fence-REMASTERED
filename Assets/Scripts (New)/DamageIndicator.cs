using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public enum damageIndicatorType
    {
        normieDamage, dankDamage, surrealDamage, mlgNoScope
    };
    public TextMeshPro damageIndicatorText;
    public damageIndicatorType damageType;
    public float dissappearTimer;

    public void RunIndicator(float damage, Vector3 position)
    {
        GameObject popup = Instantiate(this.gameObject, position, Quaternion.identity);
        popup.GetComponent<DamageIndicator>().damageIndicatorText.text = damage.ToString();
        StartCoroutine(popup.GetComponent<DamageIndicator>().IndicatorMovement(Random.Range(0, 360)));


     }

    public IEnumerator IndicatorMovement(float angle)
    {
        //transform.position = Vector2.MoveTowards(this.transform.position, targetenemy.transform.position, bscript.speed * Time.deltaTime);

        yield return new WaitForSeconds(dissappearTimer);
        damageIndicatorText.CrossFadeAlpha(0, dissappearTimer, false);
        Destroy(this.gameObject, dissappearTimer + 0.01f);

    }








}
