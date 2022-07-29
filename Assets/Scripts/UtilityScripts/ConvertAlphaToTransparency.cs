using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//This is a generic utility script to convert the alpha from a parent object and put it as a transparency value for texts.
public class ConvertAlphaToTransparency : MonoBehaviour
{
    public Image imageToInherit; //The image we want to inherit the alpha value from
    public TextMeshProUGUI textToApply; //The target text we want to apply the alpha value from the image to
    
    // Start is called before the first frame update
    void Start()
    {
        imageToInherit = GetComponentInParent<Image>();
        textToApply = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textToApply.alpha = imageToInherit.color.a;
    }
}
