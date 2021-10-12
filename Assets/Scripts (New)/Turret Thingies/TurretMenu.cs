using UnityEngine;
using Platinio.TweenEngine;
using Platinio.UI;

public class TurretMenu : MonoBehaviour
{
    [SerializeField] private float height = 0.05f;
    [SerializeField] private RectTransform canvas = null;
    [SerializeField] private RectTransform turretSelect = null; //previously turretSelect
    [SerializeField] private RectTransform turretButton = null; //previously turretButton
    [SerializeField] private float time = 1.0f;
    [SerializeField] private Ease ease = Ease.Linear;
    private Vector2 startPosition = Vector2.zero;
    private bool isVisible = false;
    private bool isBusy = false;

    private void Start()
    {
        startPosition = turretSelect.FromAnchoredPositionToAbsolutePosition(canvas);
        turretSelect.gameObject.SetActive(false);
    }

    private void Show()
    {
        turretSelect.gameObject.SetActive(true);
        turretSelect.MoveUI(new Vector2(startPosition.x, startPosition.y + height), canvas, time).SetEase(ease).SetOnComplete(delegate
        {
            isBusy = false;
            isVisible = true;
        });
        //turretButton.RotateTween(Vector3.forward, turretButton.rotation.eulerAngles.z, time).SetEase(ease);

    }

    private void Hide()
    {
        turretSelect.MoveUI(startPosition, canvas, time).SetEase(ease).SetOnComplete(delegate
        {
            isBusy = false;
            isVisible = false;
            turretSelect.gameObject.SetActive(false);
        });
    }

    public void KeepScriptActive(BuyTurretScript buyTurret)
    {
        StartCoroutine(buyTurret.DraggingUpdate());
    }

    public void Toggle()
    {
        if (isBusy)
            return;

        isBusy = true;

        if (isVisible)
            Hide();
        else
            Show();
    }



}

