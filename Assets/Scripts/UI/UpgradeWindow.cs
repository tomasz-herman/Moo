using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindow : GuiWindow
{
    public UserInterface ui;
    public Button leftButton, middleButton, rightButton;
    public UpgradeSystem upgradeSystem;

    public void Start()
    {
        leftButton.onClick.AddListener(() => OnLeftButtonClicked());
        middleButton.onClick.AddListener(() => OnMiddleButtonClicked());
        rightButton.onClick.AddListener(() => OnRightButtonClicked());
        Recalculate();
    }
    

    public void Recalculate()
    {
        bool interactable = upgradeSystem.GetPendingUpgrades() > 0;
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
