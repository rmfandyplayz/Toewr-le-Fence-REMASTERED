using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BILLBOARDSCRIPT : MonoBehaviour
{

    private Camera cam;
    public bool anchoredposition;


    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (!anchoredposition)
        {
            transform.LookAt(cam.transform);
        }
        else
        {
            transform.rotation = cam.transform.rotation;
        }

        

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);



    }
}
