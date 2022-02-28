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
        SetUpgradeButon(leftName, leftDescription, leftButton, upgrades[0]);
        SetUpgradeButon(middleName, middleDescription, middleButton, upgrades[1]);
        SetUpgradeButon(rightName, rightDescription, rightButton, upgrades[2]);
    }

    private void SetUpgradeButon(TMP_Text name, TMP_Text description, Button button, UpgradeView upgrade)
    {
        name.text = upgrade.GetName();
        name.color = UpgradeColorExtensions.GetColor(upgrade.Color);
        description.text = upgrade.GetDescription(upgradeable);
        button.image.sprite = upgrade.GetSprite();
        button.image.color = UpgradeColorExtensions.GetColor(upgrade.Color);
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
