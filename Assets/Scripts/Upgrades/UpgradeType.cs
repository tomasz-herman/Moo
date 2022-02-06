public enum UpgradeType
{
    MaxHealth, MaxAmmo, MovementSpeed,
    PistolDamage, PistolProjectileSpeed, PistolCooldown,
    ShotgunDamage, ShotgunProjectileSpeed, ShotgunCooldown, ShotgunProjectileCount, ShotgunProjectileDispersion,
    MachineGunDamage, MachineGunProjectileSpeed, MachineGunCooldown,
    GrenadeLauncherDamage, GrenadeLauncherProjectileSpeed, GrenadeLauncherCooldown,
    SwordDamage, SwordProjectileSpeed, SwordCooldown,
    //One time upgrades (do not place anything below if it's not one time upgrade)
    SwordReflectsEnemyProjectiles, 
    PistolProjectilesExplodeAfterHittingEnemy, ShotgunProjectilesExplodeAfterHittingEnemy, MachineGunProjectilesExplodeAfterHittingEnemy,
    PistolProjectilesChainToNearestEnemy, ShotgunProjectilesChainToNearestEnemy, MachineGunProjectilesChainToNearestEnemy, GrenadeLauncherProjectilesChainToNearestEnemy
}
