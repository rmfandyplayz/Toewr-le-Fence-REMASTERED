using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disable_Previous_Save_Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PersistentSaveData.Persistent.PersistentData.IsSaved("money settings.json") == false)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
