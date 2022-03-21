using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Toolbox;
using TMPro;

public class TurretButtonInitializing : MonoBehaviour
{
    //Variables Section
    // Button -> Main button for the turret button
    // Texts -> Name, Cost, Range, Damage, Cooldown
    // Image -> Turret Sprite (may be animated)
    // bool for is currently on cooldown
    [Header("References")]
    [SerializeField] private Button button;
    [SerializeField] private TurretSettings turretSettings;

    [Header("Text Input")]
    [NewLabel("Turret Name")] public TextMeshProUGUI nameText;
    [NewLabel("Turret Price")] public TextMeshProUGUI priceText;
    [NewLabel("Turret Damage")] public TextMeshProUGUI damageText;
    [NewLabel("Turret Range")] public TextMeshProUGUI rangeText;
    [NewLabel("Turret Attack Cooldown")] public TextMeshProUGUI attackCooldownText;
    [NewLabel("Turret Deploy Cooldown")] public TextMeshProUGUI deployCooldownText; // This is a feature for future development

    [Header("Images")]
    [NewLabel("Turret Preview Image")] public Image turretIcon;
    [NewLabel("Is the sprite/image animated?")] public bool isAnimated = false;
    [ShowIf(nameof(isAnimated), true), NewLabel("Place your animated sprite here")] public AtlasAnimator animatedSprite;

    [Header("Extra Button Attributes")]
    [SerializeField, NewLabel("Is the button on cooldown?")] private bool isButtonOnCooldown = false;

    // Functions Section

    //Initialize the button
    public void InitializeButton(TurretSettings turretSettings)
    {
        this.turretSettings = turretSettings;
        nameText.text = turretSettings.turretName;
        priceText.text = turretSettings.buyPrice.ToString();
        damageText.text = turretSettings.bulletSetup.bulletDamage.ToString();
        rangeText.text = turretSettings.range.ToString();
        attackCooldownText.text = turretSettings.fireRate.ToString();
        deployCooldownText.text = turretSettings.deploymentCooldown.ToString(); //May be updated later
        turretIcon.sprite = turretSettings.turretSprite;
    }

}
