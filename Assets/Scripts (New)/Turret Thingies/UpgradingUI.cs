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
    [Header("UpgradingStuff")]
    public List<Button> buttonHolder;


    
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
        foreach (Button button in buttonHolder)
        {
            button.gameObject.SetActive(false);
        }
        panel.gameObject.SetActive(false);
    }

    public void DetectUpgrade(TypeOfUpgrade upgradeKey)
    {
        if (tconfig.upgradesCounter.ContainsKey(upgradeKey))
        {
                    tconfig.BuyUpgrade(upgradeKey);
                    tconfig.upgradesCounter[upgradeKey].Counter++;
                    tconfig.ApplyUpgrade();

        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (tconfig != null)
        {
            int buttonIndex = 0;
            foreach(var counterPair in tconfig.upgradesCounter)
            {
                if (buttonIndex >= buttonHolder.Count)
                {
                    break;
                }
                var button = buttonHolder[buttonIndex];
                button.gameObject.SetActive(true);
                button.onClick.RemoveAllListeners();
                var price = counterPair.Value.Upgrade.GetPrice(counterPair.Value.Counter);
                if (GoldManager.CurrentGold() < price)
                {
                    button.interactable = false;
                    button.GetComponentInChildren<TextMeshProUGUI>().text = $"{counterPair.Value.Name} \n {price}";
                }
                else if (tconfig.upgradesCounter[counterPair.Key].Counter >= tconfig.upgradesCounter[counterPair.Key].Upgrade.MaxLevel)
                {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = $"{counterPair.Value.Name} \n MAXED OUT!";
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                    button.GetComponentInChildren<TextMeshProUGUI>().text = $"{counterPair.Value.Name} \n {price}";
                    button.onClick.AddListener(() => { DetectUpgrade(counterPair.Key); });
                }
                buttonIndex++;


            }


            textConfig.turretsprite.texture = tconfig.tsettings.turretSprite.texture;
            textConfig.turretName.text = $"{tconfig.name}";
            textConfig.damage.text = $"{tconfig.tsettings.bullet.bulletDamage.GetUpgradedValue(tconfig.CounterValue(TypeOfUpgrade.BulletDamage))}";
            textConfig.range.text = $"{tconfig.tsettings.range.GetUpgradedValue(tconfig.CounterValue(TypeOfUpgrade.Range))}";
            textConfig.attackCooldown.text = $"{tconfig.tsettings.fireRate.GetUpgradedValue(tconfig.CounterValue(TypeOfUpgrade.FireRate)):f2}";
            textConfig.dankDmgChance.text = $"{tconfig.tsettings.bullet.dankDmgChance.GetUpgradedValue(tconfig.CounterValue(TypeOfUpgrade.DankChance)):f2}%";
            textConfig.surrealDamageChance.text = $"{tconfig.tsettings.bullet.surrealDmgChance.GetUpgradedValue(tconfig.CounterValue(TypeOfUpgrade.SurrealChance)):f2}%";
            textConfig.noScopeChance.text = $"{tconfig.tsettings.bullet.NoscopeDmgChance.GetUpgradedValue(tconfig.CounterValue(TypeOfUpgrade.NoScopeChance)):f2}%";
        }


    }


    public void OpenPanel()
    {
        panel.gameObject.SetActive(true);
        panel.MoveUI(new Vector2(startPosition.x, startPosition.y + height), canvas, slideSpeed).SetEase(ease).SetOnComplete(delegate
        {
            isBusy = false;
            isVisible = true;
        });
        UpdateUI();

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
