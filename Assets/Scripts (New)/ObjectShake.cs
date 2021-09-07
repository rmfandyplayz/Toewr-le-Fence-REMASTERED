using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;

public class ObjectShake : MonoBehaviour
{
    public bool isRadialShake;
    [ShowIf(nameof(isRadialShake), true)]
    public float rotateSpeed = 75;
    [ShowIf(nameof(isRadialShake), true)]
    public float maxRotation = 20;

    public bool isLinearShake;
    [ShowIf(nameof(isLinearShake), true)]
    public float linearShakeSpeed;
    [ShowIf(nameof(isLinearShake), true)]
    public Vector2 maxLinearShake;
    [ShowIf(nameof(isLinearShake), true)]
    [SerializeField] private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        //StartCoroutine(Shake());
    }
    private void Update()
    {
        if (isRadialShake == true)
        {
            transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time * rotateSpeed) * maxRotation);
        }
        if(isLinearShake == true)
        {
            transform.position +=  new Vector3(Mathf.Sin(Time.time * linearShakeSpeed) * maxLinearShake.x, Mathf.Sin(Time.time * linearShakeSpeed) * maxLinearShake.y, 0);
            startingPosition = transform.position;
        }
    }

    IEnumerator Shake()
    {
        for(int i = 0; i < 10000; i++) //for i in range
        {
            transform.eulerAngles =new Vector3(0,0,  Mathf.Sin(Time.time * rotateSpeed) * maxRotation);
            Debug.Log(Mathf.Sin(Time.time * rotateSpeed) * maxRotation);
            yield return new WaitForEndOfFrame();
        }
    }
}
