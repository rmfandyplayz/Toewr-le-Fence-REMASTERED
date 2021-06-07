using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copytoclipboard : MonoBehaviour
{
    public string copyText;

    public void OpenLinkInBrowser()
    {
        Application.OpenURL(copyText);
    }

}
