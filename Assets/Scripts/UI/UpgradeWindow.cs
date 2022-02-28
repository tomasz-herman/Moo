using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindow : GuiWindow
{
    public UserInterface ui;
    public Button leftButton, middleButton, rightButton;
    public TMP_Text leftName, middleName, rightName;
    public TMP_Text leftDescription, middleDescription, rightDescription;
    public TMP_Text pendingUpgradesText;
    public Color upgradePendingColor, noUpgradeColor;
    public IUpgradeable upgradeable;
    private UpgradeView[] upgrades;

    public override void Awake()
    {
        base.Awake();
        leftButton.onClick.AddListener(() => OnLeftButtonClicked());
        middleButton.onClick.AddListener(() => OnMiddleButtonClicked());
        rightButton.onClick.AddListener(() => OnRightButtonClicked());
        upgradeable = FindObjectOfType<Player>();
        Recalculate();
    }
    

    public void Recalculate()
    {
        int pendingUpgrades = upgradeable.UpgradeSystem.GetPendingUpgrades();
        
        pendingUpgradesText.text = $"({pendingUpgrades})";
        if (pendingUpgrades == 0)
            pendingUpgradesText.color = noUpgradeColor;
        else
            pendingUpgradesText.color = upgradePendingColor;

        bool interactable = pendingUpgrades > 0;
        leftButton.interactable = interactable;
        middleButton.interactable = interactable;
        rightButton.interactable = interactable;

        upgrades = upgradeable.UpgradeSystem.GenerateRandomUpgrades();
        leftName.text = upgrades[0].GetName();
        leftName.color = UpgradeColorExtensions.GetColor(upgrades[0].Color);
        leftDescription.text = upgrades[0].GetDescription(upgradeable);
        leftButton.image.sprite = upgrades[0].GetSprite();
        leftButton.image.color = UpgradeColorExtensions.GetColor(upgrades[0].Color);

        middleName.text = upgrades[1].GetName();
        middleName.color = UpgradeColorExtensions.GetColor(upgrades[1].Color);
        middleDescription.text = upgrades[1].GetDescription(upgradeable);
        middleButton.image.sprite = upgrades[1].GetSprite();
        middleButton.image.color = UpgradeColorExtensions.GetColor(upgrades[1].Color);

        rightName.text = upgrades[2].GetName();
        rightName.color = UpgradeColorExtensions.GetColor(upgrades[2].Color);
        rightDescription.text = upgrades[2].GetDescription(upgradeable);
        rightButton.image.sprite = upgrades[2].GetSprite();
        rightButton.image.color = UpgradeColorExtensions.GetColor(upgrades[2].Color);
    }

    private void OnLeftButtonClicked()
    {
        upgradeable.UpgradeSystem.Upgrade(upgrades[0]);
        OnAnyButtonClicked();
    }
    private void OnMiddleButtonClicked()
    {
        upgradeable.UpgradeSystem.Upgrade(upgrades[1]);
        OnAnyButtonClicked();
    }
    private void OnRightButtonClicked()
    {
        upgradeable.UpgradeSystem.Upgrade(upgrades[2]);
        OnAnyButtonClicked();
    }
    private void OnAnyButtonClicked()
    {
        upgradeable.UpgradeSystem.RemoveUpgrade();
        Recalculate();
        if (upgradeable.UpgradeSystem.GetPendingUpgrades() == 0)
            ui.TryCloseWindow(this);
    }

    public void Open()
    {
        ui.TryOpenWindow(this);
    }
}
