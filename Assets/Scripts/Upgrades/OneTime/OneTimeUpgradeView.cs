using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OneTimeUpgradeView : UpgradeView
{
    public OneTimeUpgradeView(string name, UpgradeType type)
           : base(name, type)
    {
        
    }

    public override sealed float GetScalingFactor(int upgradeCount) { return 1f; }

    protected override sealed void CommitUpdate(IUpgradeable upgradeable, float newFactor)
    {
        CommitUpdate(upgradeable);
    }

    protected override sealed string GetDescription(IUpgradeable upgradeable, float newFactor)
    {
        return GetDescription(upgradeable);
    }

    protected abstract void CommitUpdate(IUpgradeable upgradeable);
    protected abstract new string GetDescription(IUpgradeable upgradeable);
}
