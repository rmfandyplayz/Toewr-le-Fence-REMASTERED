using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pathblockers : MonoBehaviour
{
    public float Overall_Health = 50;
    public float Current_Health;
    public Image Health_Bar;
    public Transform target;
    public float freezeDuration;

    // Start is called before the first frame update
    void Start()
    {
        Current_Health = Overall_Health;
        Health_Bar.fillAmount = Current_Health / Overall_Health;
    }
    // Update is called once per frame
    void Update()
    {
        if (Current_Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            Damage(collision.gameObject.GetComponent<Enemy>().TakeHP());
            float ehp = collision.gameObject.GetComponent<Enemy>().TakeHP();

                target = collision.gameObject.transform;
                target.GetComponent<Enemy>().TakeDamage(ehp,0, freezeDuration);
            
            Debug.Log(collision.gameObject.GetComponent<Enemy>().TakeHP());
        }
    }
    void Damage(float D)
    {
        Current_Health -= D;
        Health_Bar.fillAmount = Current_Health / Overall_Health;
    }
}
