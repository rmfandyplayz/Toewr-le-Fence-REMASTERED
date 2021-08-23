using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpriteHandler : MonoBehaviour
{
    public Color normalColor = Color.white;
    public Color invalidColor = Color.red;
    private SpriteRenderer sr;
    private CircleCollider2D collider;
    private bool isInvalid = false;
    public void Initialize(Sprite newSprite, Obj2DSimpleInfo info)
    {
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
        collider.offset = info.position;
        collider.radius = info.value;
        sr.sprite = newSprite;
        isInvalid = false;
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

    public bool CheckIfHitObstacles()
    {
        Collider2D col = GetComponent<Collider2D>();
        RaycastHit2D[] hit2Ds = new RaycastHit2D[10];
        if(col != null && col.Cast(Vector2.zero, hit2Ds) > 0)
        {
            foreach(var hit in hit2Ds)
            {
                if(hit.collider == null) continue;

                // Debug.Log(hit.collider.GetComponent<TurretSpriteHandler>());

                if(hit.collider.CompareTag("LevelObstacle") || hit.collider.GetComponent<TurretSpriteHandler>() != null)
                {
                    StartedHittingObstacle();
                    return true;
                }
            }
        }
        StoppedHittingObstacle();
        return false;
    }

    public void StartedHittingObstacle()
    {
        if(!isInvalid)
        {
            normalColor = sr.color;
            sr.color = invalidColor;
            isInvalid = true;
        }
    }

    public void StoppedHittingObstacle()
    {
        if(isInvalid)
        {
            sr.color = normalColor;
            isInvalid = false;
        }
    }
}
