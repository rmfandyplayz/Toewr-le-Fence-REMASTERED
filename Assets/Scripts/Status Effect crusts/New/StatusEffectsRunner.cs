using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;
using UnityEngine.UI;


public class StatusEffectsRunner : MonoBehaviour
{
    //Variables Section
    public StatusEffectsScriptObj scriptableObjReference;
    public Image statusEffectImage; //The actual image component, NOT the visible sprite

    public void InitializeEffect(StatusEffectsScriptObj scriptableObjReference)
    {
        this.scriptableObjReference = scriptableObjReference;
        if(scriptableObjReference.isAnimatedActiveIcon == true)
        {
            //statusEffectImage.gameObject = scriptableObjReference.animatedActiveIcon;

            //TO DO:
            //Add AtlasAnimator integration to this game object on the same level as this script
        }
        else
        {
            statusEffectImage.sprite = scriptableObjReference.activeIcon;
        }
    }

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        
    }

}
