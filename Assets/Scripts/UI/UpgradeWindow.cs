using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindow : GuiWindow
{
    public UserInterface ui;
    public Button leftButton, middleButton, rightButton;
    public TMP_Text pendingUpgradesText;
    public Color upgradePendingColor, noUpgradeColor;
    public UpgradeSystem upgradeSystem;

    new void Start()
    {
        base.Start();
        leftButton.onClick.AddListener(() => OnLeftButtonClicked());
        middleButton.onClick.AddListener(() => OnMiddleButtonClicked());
        rightButton.onClick.AddListener(() => OnRightButtonClicked());
        Recalculate();
    }
    

    public void Recalculate()
    {
        int pendingUpgrades = upgradeSystem.GetPendingUpgrades();
        
        pendingUpgradesText.text = $"({pendingUpgrades})";
        if (pendingUpgrades == 0)
            pendingUpgradesText.color = noUpgradeColor;
        else
            pendingUpgradesText.color = upgradePendingColor;

        bool interactable = pendingUpgrades > 0;
        leftButton.interactable = interactable;
        middleButton.interactable = interactable;
        rightButton.interactable = interactable;
    }

    private void OnLeftButtonClicked()
    {
        upgradeSystem.Upgrade(0);
        OnAnyButtonClicked();
    }
    private void OnMiddleButtonClicked()
    {
        upgradeSystem.Upgrade(1);
        OnAnyButtonClicked();
    }
    private void OnRightButtonClicked()
    {
        upgradeSystem.Upgrade(2);
        OnAnyButtonClicked();
    }
    private void OnAnyButtonClicked()
    {
        upgradeSystem.RemoveUpgrade();
        Recalculate();
        if (upgradeSystem.GetPendingUpgrades() == 0)
            ui.TryCloseWindow(this);
    }

    public void Open()
    {
        ui.TryOpenWindow(this);
    }
}
