using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower_Health : MonoBehaviour {
    public float Overall_Health = 50;
    public float Current_Health;
    public Image Health_Bar;
    

	// Use this for initialization
	void Start () {
        Current_Health = Overall_Health;
        Health_Bar.fillAmount = Current_Health / Overall_Health;
	}
	
	// Update is called once per frame
	void Update () {
		if (Current_Health <= 0)
        {
            SceneManager.LoadScene("Lose Scene");
        }
	}
    public void Damage(float D)
    {
        Debug.Log(D);
        Debug.Log(Current_Health);
        Current_Health -= D;
        Health_Bar.fillAmount = Current_Health / Overall_Health;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<Enemy>() != null) 
        {
            Debug.Log(collision.gameObject.GetComponent<Enemy>().TakeHP());
            Damage(collision.gameObject.GetComponent<Enemy>().TakeHP());
        }
    }
   
}