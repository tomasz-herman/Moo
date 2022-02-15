using UnityEngine;

public abstract class UpgradeView
{
    private readonly string _name;
    private readonly Sprite _sprite;
    public UpgradeType upgradeType { get; protected set; }

    public UpgradeIcon Icon { get; private set; } = UpgradeIcon.Default;
    public UpgradeColor Color { get; private set; } = UpgradeColor.White;

    protected UpgradeView(string name, UpgradeType upgradeType, UpgradeIcon icon, UpgradeColor color)
    {
        this.upgradeType = upgradeType;
        this._name = name;
        this._sprite = icon.GetSprite();
        this.Icon = icon;
        this.Color = color;
    }

    public string GetName() { return _name; }
    public abstract float GetScalingFactor(int upgradeCount);
    protected abstract string GetDescription(IUpgradeable upgradeable, float newFactor);
    public string GetDescription(IUpgradeable upgradeable)
    {
        int newCount = upgradeable.UpgradeSystem.GetUpgradeCount(upgradeType) + 1;
        return GetDescription(upgradeable, GetScalingFactor(newCount));
    }
    public Sprite GetSprite() { return _sprite; }
    public void OnUpgraded(IUpgradeable upgradeable)
    {
        CommitUpdate(upgradeable, GetScalingFactor(upgradeable.UpgradeSystem.GetUpgradeCount(upgradeType)));
    }

    protected abstract void CommitUpdate(IUpgradeable upgradeable, float newFactor);
}
