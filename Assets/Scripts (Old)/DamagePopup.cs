using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamagePopup : MonoBehaviour
{

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    public Transform textPrefab;
    public float timer;




    public static DamagePopup Create(Vector3 position, float damageAmount)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup.transform, position, Quaternion.Euler(90,0,0));
        //Transform damagePopupTransform = Instantiate(textPrefab, position, Quaternion.identity);
        //GameObject temp = Instantiate(GameAssets.i.pfDamagePopup.transform, position, Quaternion.identity);
        //temp.name = "New DamagePopup";
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        textColor = textMesh.color;
        timer = 0f;
    }

    public void Setup(float damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        disappearTimer = 0.65f;
    }

    private void Update()
    {
        float moveYSpeed = 20f;
        if (timer <= disappearTimer/2) {
            transform.position += new Vector3(0, 0, moveYSpeed) * Time.deltaTime;
                }
        else
        {
            transform.position += new Vector3(0, 0, -moveYSpeed) * Time.deltaTime;
        }


        disappearTimer -= Time.deltaTime;

        timer += Time.deltaTime;

        if (disappearTimer < 0)
        {
            float disappearSpeed = 100f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }


}
