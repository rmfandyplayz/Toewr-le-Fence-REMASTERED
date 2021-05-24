using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationValue;


    void Update()
    {
        transform.eulerAngles = rotationValue;
    }


}
