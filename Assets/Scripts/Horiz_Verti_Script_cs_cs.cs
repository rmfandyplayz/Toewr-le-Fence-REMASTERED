using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horiz_Verti_Script_cs_cs : MonoBehaviour
{
    public bool Isverticalpath = true;

    void Start()
    {
        
    }

    void Update()
    {
       
    }


    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.tag == "Pathblockers")
        {
            transform.Rotate(0, 90, 0);
        }
    }

}
