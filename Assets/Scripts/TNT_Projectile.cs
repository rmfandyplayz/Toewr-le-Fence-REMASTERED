using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT_Projectile : MonoBehaviour
{
    public float TNTFuse = 3f;
    public GameObject TNTExplosionPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void PUpdate()
    {
        TNTFuse = TNTFuse - Time.deltaTime;
        if (TNTFuse <= 0)
        {
            GameObject clone = Instantiate(TNTExplosionPrefab, transform.position, transform.rotation);
            Destroy(clone, 2.5f);
            TNTFuse = TNTFuse;
            
        }
    }
    
}
