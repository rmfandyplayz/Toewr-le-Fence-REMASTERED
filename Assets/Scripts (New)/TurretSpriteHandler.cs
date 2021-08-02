using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpriteHandler : MonoBehaviour
{
    public Color normalColor = Color.white;
    public Color invalidColor = Color.red;
    private SpriteRenderer sr;
    public void Initialize(Sprite newSprite)
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = newSprite;
    }

    public void ToggleTransparency(bool enable, float transparency = 1f)
    {
        if(enable == true)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Clamp01(transparency));
        }
        else
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        }
    }

    private void OnMouseEnter() {
        Debug.Log(MouseHoverCounter.numberOfObjectsUnderMouse + "from TurretSpriteHandler.cs");
        MouseHoverCounter.numberOfObjectsUnderMouse++;
        normalColor = sr.color;
        if (MouseHoverCounter.numberOfObjectsUnderMouse > 1)
        {
            sr.color = invalidColor;
        }

    }

    private void OnMouseExit() {
        MouseHoverCounter.numberOfObjectsUnderMouse--;
        sr.color = normalColor;
    }
}
