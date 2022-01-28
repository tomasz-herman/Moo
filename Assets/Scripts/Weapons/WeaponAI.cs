using System;

namespace Assets.Scripts.Weapons
{
    public enum WeaponAI
    {
        PistolAI,
        SwordAI,
        MachineGunAI,
        ShotgunAI,
        GrenadeAI
    }
    
    public abstract class WeaponAIProperties
    {
        public static WeaponAIProperties Get(WeaponAI weaponAI) =>
            weaponAI switch
            {
                WeaponAI.GrenadeAI => GRENADE_AI,
                WeaponAI.SwordAI => SWORD_AI,
                WeaponAI.PistolAI => PISTOL_AI,
                WeaponAI.MachineGunAI => MACHINEGUN_AI,
                WeaponAI.ShotgunAI => SHOTGUN_AI,
                _ => throw new ArgumentOutOfRangeException(nameof(weaponAI), weaponAI, null)
            };

        private static readonly WeaponAIProperties SWORD_AI = new SwordAIProperties();
        private static readonly WeaponAIProperties PISTOL_AI = new PistolAIProperties();
        private static readonly WeaponAIProperties GRENADE_AI = new GrenadeAIProperties();
        private static readonly WeaponAIProperties MACHINEGUN_AI = new MachineGunAIProperties();
        private static readonly WeaponAIProperties SHOTGUN_AI = new ShotgunAIProperties();

        public abstract float PreferredRange { get; }
        public abstract float MaximumRange { get; }
        public abstract float Timeout { get; }
        public abstract float ProjectileSpeed { get; }
        public virtual float BonusMovementSpeed => 1.0f;
        public virtual float BonusDamage => 1.0f;
        public abstract Type Type { get; }
    
        private WeaponAIProperties() { }
    
        private class PistolAIProperties : WeaponAIProperties
        {
            public override float PreferredRange => 7.5f;
            public override float MaximumRange => 40;
            public override float Timeout => 1;
            public override float ProjectileSpeed => 6.5f;
            public override Type Type => typeof(Pistol);
            public override float BonusDamage => 0.65f;
        }
    
        private class SwordAIProperties : WeaponAIProperties
        {
            public override float PreferredRange => 1.5f;
            public override float MaximumRange => 3.5f;
            public override float Timeout => 1;
            public override float ProjectileSpeed => 10;
            public override Type Type => typeof(Sword);
            public override float BonusMovementSpeed => 2.5f;
            public override float BonusDamage => 0.5f;
        }
    
        private class GrenadeAIProperties : WeaponAIProperties
        {
            public override float PreferredRange => 15;
            public override float MaximumRange => 25;
            public override float Timeout => 0.5f;
            public override float ProjectileSpeed => 10;
            public override Type Type => typeof(GrenadeLauncher);
            public override float BonusDamage => 1.5f;
        }
    
        private class MachineGunAIProperties : WeaponAIProperties
        {
            public override float PreferredRange => 15;
            public override float MaximumRange => 40;
            public override float Timeout => 1;
            public override float ProjectileSpeed => 5;
            public override Type Type => typeof(MachineGun);
            public override float BonusDamage => 0.25f;
        }
    
        private class ShotgunAIProperties : WeaponAIProperties
        {
            public override float PreferredRange => 2;
            public override float MaximumRange => 16;
            public override float Timeout => 0.75f;
            public override float ProjectileSpeed => 5;
            public override Type Type => typeof(Shotgun);
            public override float BonusDamage => 1.5f;
        }
    }

}