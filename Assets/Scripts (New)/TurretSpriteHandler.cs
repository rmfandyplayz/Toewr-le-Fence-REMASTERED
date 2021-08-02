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

    private void OnMouseEnter() {
        MouseHoverCounter.numberOfObjectsUnderMouse++;
        if(MouseHoverCounter.numberOfObjectsUnderMouse > 1)
        {
            sr.color = invalidColor;
        }

    }

    private void OnMouseExit() {
        MouseHoverCounter.numberOfObjectsUnderMouse--;
        sr.color = normalColor;
    }
}
