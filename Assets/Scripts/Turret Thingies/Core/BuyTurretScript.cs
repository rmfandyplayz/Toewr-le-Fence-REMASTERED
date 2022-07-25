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
    private TurretSpriteHandler tsh;
    private bool isDragging = false;
    private Camera camClickDetect;
    private TurretSettings stns;

    public UnityEvent OnClickBuyButton;
    public UnityEvent OnPlaceTurret;
    public UnityEvent OnDiscardTurret;
    public UnityEvent OnNotAbleToBuy;

    void Start() 
    {
        camClickDetect = Camera.main;
    }

    // void Update()
    // {
    //     if(isDragging)
    //     {
    //         turret.transform.position = camera.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
    //         if(Input.GetKeyDown(placeTurretKey) && MouseHoverCounter.numberOfObjectsUnderMouse <= 1)
    //         {
    //             isDragging = false;
    //             turret.GetComponent<TurretConfiguration>().enabled = true;
    //             turret.GetComponentInChildren<TurretSpriteHandler>().ToggleTransparency(false);
    //             turret = null;
    //             GoldManager.instance.AddGold(-stns.buyPrice);
    //         }
    //         else if(Input.GetKeyDown(stopPlacementKey))
    //         {
    //             isDragging = false;
    //             Destroy(turret);
    //             turret = null;
    //         }
    //     }
    // }

    public void BuyTurret(TurretSettings settings)
    {
        stns = settings;
        if(GoldManager.CurrentGold() >= stns.buyPrice && !isDragging && turret == null)
        {
            if (turret != null)
            {
                Destroy(turret);
            }
            turret = Instantiate(turretPlaceholder);
            TurretConfiguration config = turret.GetComponent<TurretConfiguration>();
            config.tsettings = settings;
            config.Initialize(showTurretRange: true);
            config.enabled = false;
            tsh = turret.GetComponentInChildren<TurretSpriteHandler>();
            tsh.ToggleTransparency(true, 0.4f);
            isDragging = true;
            OnClickBuyButton?.Invoke();
        }
        else if (GoldManager.CurrentGold() < stns.buyPrice)
        {
            OnNotAbleToBuy?.Invoke();
        }
    }

    public IEnumerator DraggingUpdate()
    {
        while(isDragging)
         {

            turret.transform.position = camClickDetect.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
            if(!tsh.CheckIfHitObstacles() && Input.GetKey(placeTurretKey))
            {
                isDragging = false;
                var upgradingUI = FindObjectOfType<UpgradingUI>();
                var turretConfig = turret.GetComponent<TurretConfiguration>();

                turret.GetComponent<TurretConfiguration>().enabled = true;
                turret.GetComponentInChildren<TurretSpriteHandler>().ToggleTransparency(false);
                turret.GetComponentInChildren<TurretRangeHandler>().ToggleRangeVisual(false);
                turret.GetComponentInChildren<TurretSpriteHandler>().OnSpriteClick.AddListener(() => { upgradingUI.tconfig = turretConfig; });
                turret.GetComponentInChildren<TurretSpriteHandler>().OnSpriteClick.AddListener(upgradingUI.Toggle);
                turret.GetComponentInChildren<TurretSpriteHandler>().isPlacing = false;
                turret = null;
                GoldManager.instance.AddGold(-stns.buyPrice);
                OnPlaceTurret?.Invoke();
            }
            else if(Input.GetKey(stopPlacementKey))
            {
                isDragging = false;
                Destroy(turret);
                turret = null;
                OnDiscardTurret?.Invoke();
            }
            yield return new WaitForEndOfFrame();
         }
    }
}
