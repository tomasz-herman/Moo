using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeable
{
    public UpgradeSystem UpgradeSystem { get; }
    public HealthSystem HealthSystem { get; }
    public AmmoSystem AmmoSystem { get; }
    public MovementSystem MovementSystem { get; }
    public Shooting ShootingSystem { get; }
}
