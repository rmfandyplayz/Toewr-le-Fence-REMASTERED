using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletSetup bscript;
    public GameObject targetenemy;
    private Camera main;
    public SerializedDictionary<TypeOfUpgrade, int> upgradesCounter;

    void Start()
    {
        main = Camera.main;
        foreach(TypeOfUpgrade upgrade in bscript.upgrades)
        {
            upgradesCounter.Add(upgrade, 0);
        } 
    }

    public void InitUpgrade(SerializedDictionary<TypeOfUpgrade, int> counter)
    {
        foreach (var pair in counter)
        {
            if(upgradesCounter.ContainsKey(pair.Key))
            {
                upgradesCounter[pair.Key] = pair.Value;
            }
        }
    }

    void Update()
    {
        if(bscript.followTarget == true && targetenemy != null)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, targetenemy.transform.position, bscript.speed * Time.deltaTime);
            //transform.LookAt(targetenemy.transform.position, Vector3.up);
            float angle = 0;

            Vector3 relative = transform.InverseTransformPoint(targetenemy.transform.position);
            angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
            transform.Rotate(0, 0, -angle);

        }
        else
        {
            this.transform.position += transform.up * bscript.speed * Time.deltaTime;
        }

        Vector2 screenPosition = main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            Destroy(this.gameObject);
        }

        if (screenPosition.x > Screen.width || screenPosition.x < 0)
        {
            Destroy(this.gameObject);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            Destroy(this.gameObject);
            enemy.TakeDamage(bscript);
        }
    }



}