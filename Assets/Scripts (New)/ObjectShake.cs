using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    public float switchTime = 0.25f;
    public float maxRotation = 30;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        while(true)
        {
           transform.eulerAngles =new Vector3(0,0,  Mathf.Sin(Time.time * switchTime) * maxRotation);
        }
    }
}
