using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Toolbox;

public class TabInitializer : MonoBehaviour
{
    //Variables Section

    public TextMeshProUGUI tabName;
    public Image tabBackground;
    [Tooltip("If  bool 'expands' is enabled in TabScriptableObject.cs, then fill in this variable.")] public Image tabExpansionImage;

    //Functions Section

    public void InitializeTab(TabScriptableObject tab)
    {
        tabName.text = tab.tabName;
        tabBackground.sprite = tab.tabBackgroundImage;
        if (tab.expands)
        {
            tabExpansionImage.sprite = tab.tabBackgroundImageExpansionChunk;
        }
    }

}
