using UnityEngine;

public abstract class UpgradeView
{
    private readonly string _name;
    private readonly Sprite _sprite;
    public UpgradeType upgradeType { get; protected set; }

    protected UpgradeView(string name, UpgradeType upgradeType)
    {
        this.upgradeType = upgradeType;
        this._name = name;

        var data = ApplicationData.UpgradeData[upgradeType];
        this._sprite = data.image;
    }

    public string GetName() { return _name; }
    public abstract float GetScalingFactor(int upgradeCount);
    protected abstract string GetDescription(IUpgradeable upgradeable, float oldFactor, float newFactor);
    public string GetDescription(IUpgradeable upgradeable)
    {
        int currentCount = upgradeable.UpgradeSystem.GetUpgradeCount(upgradeType);
        int newCount = currentCount + 1;
        return GetDescription(upgradeable, GetScalingFactor(currentCount), GetScalingFactor(newCount));
    }
    public Sprite GetSprite() { return _sprite; }
    public void OnUpgraded(IUpgradeable upgradeable)
    {
        CommitUpdate(upgradeable, GetScalingFactor(upgradeable.UpgradeSystem.GetUpgradeCount(upgradeType)));
    }

    protected abstract void CommitUpdate(IUpgradeable upgradeable, float newFactor);
}
