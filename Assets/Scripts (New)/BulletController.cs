using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BulletSetup bscript;
    public GameObject targetenemy;
    private Camera main;

    void Start()
    {
        main = Camera.main;
    }

    void Update()
    {
        if(bscript.followTarget == true && targetenemy != null)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, targetenemy.transform.position, bscript.speed * Time.deltaTime);
            //transform.LookAt(targetenemy.transform.position, Vector3.up);
            float angle = 0;

            Vector3 relative = transform.InverseTransformPoint(targetenemy.transform.position);            angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;            transform.Rotate(0, 0, -angle);

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(bscript.bulletDamage);

        }
        Destroy(this.gameObject);
    }



}