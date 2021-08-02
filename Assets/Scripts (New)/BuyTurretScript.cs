using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuyTurretScript : MonoBehaviour
{
    public GameObject turretPlaceholder;
    public KeyCode placeTurretKey = KeyCode.Mouse0;
    public KeyCode stopPlacementKey = KeyCode.Mouse1;
    private static GameObject turret = null;
    private bool isDragging = false;
    private Camera camera;
    private TurretSettings stns;

    public UnityEvent OnClickBuyButton;
    public UnityEvent OnPlaceTurret;

    void Start() 
    {
        camera = Camera.main;
    }

    void Update()
    {
        if(isDragging)
        {
            turret.transform.position = camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
            if(Input.GetKeyDown(placeTurretKey) && MouseHoverCounter.numberOfObjectsUnderMouse <= 1)
            {
                isDragging = false;
                turret.GetComponent<TurretConfiguration>().enabled = true;
                turret.GetComponentInChildren<TurretSpriteHandler>().ToggleTransparency(false);
                turret = null;
                GoldManager.instance.AddGold(-stns.buyPrice);
            }
            else if(Input.GetKeyDown(stopPlacementKey))
            {
                isDragging = false;
                Destroy(turret);
                turret = null;
            }
        }
    }

    public void BuyTurret(TurretSettings settings)
    {
        stns = settings;
        if(GoldManager.GetGold() >= stns.buyPrice && !isDragging)
        {
            if (turret != null)
            {
                Destroy(turret);
            }
            turret = Instantiate(turretPlaceholder);
            TurretConfiguration config = turret.GetComponent<TurretConfiguration>();
            config.tsettings = settings;
            config.Initialize();
            config.enabled = false;
            turret.GetComponentInChildren<TurretSpriteHandler>().ToggleTransparency(true, 0.4f);
            isDragging = true;
            OnClickBuyButton?.Invoke();
        }
    }
}
