using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomUnityEvent;

public class TurretRangeHandler : MonoBehaviour
{
    [SerializeField] private float turretRange;
    [SerializeField] private SpriteRenderer rangeSprite;
    [SerializeField] private CircleCollider2D colliderRange;
    private bool isShowingRangeVisual = false;

    public UEventGameObject OnRangeEnter;
    public UEventGameObject OnRangeExit;

    public void Initialize(Obj2DSimpleInfo newRangeObj)
    {
        turretRange = newRangeObj.value;
        this.transform.position = newRangeObj.position;
        this.transform.localScale = Vector3.one * turretRange * 2;
        if(colliderRange == null)
        {
            colliderRange = GetComponent<CircleCollider2D>();
        }
        colliderRange.isTrigger = true;
        colliderRange.offset = new Vector2(transform.position.x, transform.position.y);
        colliderRange.radius = turretRange;
        if(rangeSprite == null)
        {
            rangeSprite = GetComponent<SpriteRenderer>();
        }
        ToggleRangeVisual(false);
    }

    public void UpdateRange(float newRange)
    {
        turretRange = newRange;
    }

    public void ToggleRangeVisual(bool? overrideToggle = null)
    {
        if(overrideToggle != null)
        {
            isShowingRangeVisual = overrideToggle.Value;
            rangeSprite.enabled = isShowingRangeVisual;

        }
        else
        {
            ToggleRangeVisual(!isShowingRangeVisual);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        OnRangeEnter?.Invoke(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other) {
        OnRangeExit?.Invoke(other.gameObject);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }
}
