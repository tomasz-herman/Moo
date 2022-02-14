public enum UpgradeType
{
    MaxHealth, MaxAmmo, MovementSpeed,
    PistolDamage, PistolProjectileSpeed, PistolCooldown, PistolAmmoCost,
    ShotgunDamage, ShotgunProjectileSpeed, ShotgunCooldown, ShotgunAmmoCost, ShotgunProjectileCount, ShotgunProjectileDispersion,
    MachineGunDamage, MachineGunProjectileSpeed, MachineGunCooldown, MachineGunAmmoCost,
    GrenadeLauncherDamage, GrenadeLauncherProjectileSpeed, GrenadeLauncherCooldown, GrenadeLauncherAmmoCost,
    SwordDamage, SwordProjectileSpeed, SwordCooldown,
    //One time upgrades (do not place anything below if it's not one time upgrade)
    SwordReflectsEnemyProjectiles, 
    //PistolProjectilesExplodeAfterHittingEnemy, ShotgunProjectilesExplodeAfterHittingEnemy, MachineGunProjectilesExplodeAfterHittingEnemy,
    PistolProjectilesChainToNearestEnemy, ShotgunProjectilesChainToNearestEnemy, MachineGunProjectilesChainToNearestEnemy, GrenadeLauncherProjectilesChainToNearestEnemy
}

public enum UpgradeIcon
{
    Default, MaxAmmo, MaxHealth, MovementSpeed, WeaponDamage, ProjectileSpeed, WeaponCooldown, AmmoCost,
    ShotgunProjectileCount, ShotgunProjectileDispersion, Lightsaber, ExplodingProjectiles, Chaining
}

public enum UpgradeColor
{
    Shotgun, Pistol, MachineGun, Sword, GrenadeLauncher, OneTime, White
}