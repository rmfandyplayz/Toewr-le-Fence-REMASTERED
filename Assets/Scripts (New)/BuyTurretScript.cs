using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTurretScript : MonoBehaviour
{
    public GameObject turretPlaceholder;
    public KeyCode placeTurretKey = KeyCode.Mouse0;
    public KeyCode stopPlacementKey = KeyCode.Mouse1;
    private GameObject turret = null;
    private bool isDragging = false;
    private Camera camera;

    void Start() 
    {
        camera = Camera.main;
    }

    void Update()
    {
        if(isDragging)
        {
            turret.transform.position = camera.ScreenToWorldPoint(Input.mousePosition);
            if(Input.GetKeyDown(placeTurretKey) && MouseHoverCounter.numberOfObjectsUnderMouse <= 1)
            {
                isDragging = false;
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
        if(GoldManager.GetGold() >= settings.buyPrice)
        {
            turret = Instantiate(turretPlaceholder);
            TurretConfiguration config = turret.GetComponent<TurretConfiguration>();
            config.tsettings = settings;
            config.Initialize();
            isDragging = true;
        }
    }
}
