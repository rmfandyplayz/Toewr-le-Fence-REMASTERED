using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretSpriteHandler : MonoBehaviour
{
    public Color normalColor = Color.white;
    public Color invalidColor = Color.red;
    public bool isPlacing = true;
    public float busyTimer = 0.5f;
    private SpriteRenderer sr;
    private CircleCollider2D tcollider;
    private bool isInvalid = false;
    private bool isBusy = false;
    public UnityEvent OnSpriteClick = new UnityEvent();



    public void Initialize(Sprite newSprite, Obj2DSimpleInfo info)
    {
        sr = GetComponent<SpriteRenderer>();
        tcollider = GetComponent<CircleCollider2D>();
        tcollider.offset = info.position;
        tcollider.radius = info.value;
        sr.sprite = newSprite;
        isInvalid = false;
        isPlacing = true;
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

    private void OnMouseDown() {
        Debug.LogWarning("Sprite was clicked / " + isPlacing);
        if (!isPlacing && !isBusy)
        {
            StartCoroutine(BusySprite());
            OnSpriteClick?.Invoke();
            
        }
        else
        {
             return;
        }
    }

    private IEnumerator BusySprite()
    {
        isBusy = true;
        yield return new WaitForSeconds(busyTimer);
        isBusy = false;
    }
}
