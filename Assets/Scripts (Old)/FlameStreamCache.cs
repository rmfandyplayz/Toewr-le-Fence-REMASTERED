using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStreamCache : MonoBehaviour
{
    private AllTurrets flamestreamparent;

    public void SetParent(AllTurrets parent)
    {
        flamestreamparent = parent;
    }


    public AllTurrets GetParent()
    {
        return flamestreamparent;
    }






}
