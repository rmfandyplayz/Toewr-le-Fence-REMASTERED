using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platinio.TweenEngine;
using Platinio.UI;
using TMPro;
using Toolbox;
using UnityEngine.UI;

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
    public TurretConfiguration tconfig = null;
    
    [System.Serializable]
    public class UpgradePanelTextConfig
    {
        public RawImage turretsprite;
        public TextMeshProUGUI turretName;
        public TextMeshProUGUI damage;
        public TextMeshProUGUI range;
        public TextMeshProUGUI attackCooldown;
        public TextMeshProUGUI dankDmgChance;
        public TextMeshProUGUI surrealDamageChance;
        public TextMeshProUGUI noScopeChance;
    }
    public UpgradePanelTextConfig textConfig;

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
        if(tconfig != null)
        {
            textConfig.turretsprite.texture = tconfig.tsettings.turretSprite.texture;
            textConfig.turretName.text = $"{tconfig.name}";
            textConfig.damage.text = $"{tconfig.tsettings.bullet.bulletDamage.GetUpgradedValue(0)}";
            textConfig.range.text = $"{tconfig.tsettings.range.GetUpgradedValue(0)}";
            textConfig.attackCooldown.text = $"{tconfig.tsettings.fireRate.GetUpgradedValue(0)}";
            textConfig.dankDmgChance.text = $"{tconfig.tsettings.bullet.dankDmgChance}%";
            textConfig.surrealDamageChance.text = $"{tconfig.tsettings.bullet.surrealDmgChance}%";
            textConfig.noScopeChance.text = $"{tconfig.tsettings.bullet.NoscopeDmgChance}%";
        }

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

    public void DeactivatePanel()
    {
        ClosePanel();
        if(tconfig != null)
        {
            tconfig.rangeHolder.ToggleRangeVisual(false);
        }
    }

    public void SellTurret()
    {
        int refund = Mathf.CeilToInt((float) tconfig.tsettings.buyPrice / 2.0f);
        GoldManager.instance.AddGold(refund);
        Destroy(tconfig.gameObject);
        DeactivatePanel();
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
