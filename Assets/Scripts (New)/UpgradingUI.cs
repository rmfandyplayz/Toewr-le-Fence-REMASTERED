using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platinio.TweenEngine;
using Platinio.UI;

public class UpgradingUI : MonoBehaviour
{
    public RectTransform panel;
    public float slideSpeed;
    public float height;
    [SerializeField] private RectTransform canvas = null;
    [SerializeField] private Ease ease = Ease.Linear;
    private Vector2 startPosition = Vector2.zero;
    private bool isVisible = false;
    private bool isBusy = false;
    

    private void Start()
    {
        startPosition = panel.FromAnchoredPositionToAbsolutePosition(canvas);
        panel.gameObject.SetActive(false);
    }

    public void OpenPanel()
    {
        panel.gameObject.SetActive(true);
        panel.MoveUI(new Vector2(startPosition.x, startPosition.y + height), canvas, slideSpeed).SetEase(ease).SetOnComplete(delegate
        {
            isBusy = false;
            isVisible = true;
        });

    }

    public void ClosePanel()
    {
        panel.MoveUI(startPosition, canvas, slideSpeed).SetEase(ease).SetOnComplete(delegate
        {
            isBusy = false;
            isVisible = false;
            panel.gameObject.SetActive(false);
        });
    }


    public void Toggle()
    {
        if(isBusy == true)
        {
            return;
        }
        isBusy = true;
        if (isVisible == true)
        {
            ClosePanel();
        }
        else
        {
            OpenPanel();
        }
    }

}
