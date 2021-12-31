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
    
        public abstract float Range { get; }
        public abstract float Timeout { get; }
        public abstract Type Type { get; }
    
        private WeaponAIProperties() { }
    
        private class PistolAIProperties : WeaponAIProperties
        {
            public override float Range => 20;
            public override float Timeout => 1.5f;
            public override Type Type => typeof(Pistol);
        }
    
        private class SwordAIProperties : WeaponAIProperties
        {
            public override float Range => 2.5f;
            public override float Timeout => 1.5f;
            public override Type Type => typeof(Sword);
        }
    
        private class GrenadeAIProperties : WeaponAIProperties
        {
            public override float Range => 15;
            public override float Timeout => 4;
            public override Type Type => typeof(Grenade);
        }
    
        private class MachineGunAIProperties : WeaponAIProperties
        {
            public override float Range => 25;
            public override float Timeout => 0.5f;
            public override Type Type => typeof(MachineGun);
        }
    
        private class ShotgunAIProperties : WeaponAIProperties
        {
            public override float Range => 16;
            public override float Timeout => 3;
            public override Type Type => typeof(Shotgun);
        }
    }

}