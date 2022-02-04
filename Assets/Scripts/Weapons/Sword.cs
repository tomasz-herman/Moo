using Assets.Scripts.SoundManager;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Sword : Weapon
    {
        private readonly Blade bladePrefab;
        private Color color = Color.green;


        public override float projectileSpeed { get; set; } = 30f;
        public override float triggerTimeout { get; set; } = 1f;
        public override float baseDamage { get; set; } = 1f;

        public override string Name { get; set; } = "Sword";
        protected override int ammoConsumption => 0;

        public Sword(Blade bladePrefab): base(WeaponType.Sword, SoundType.SwordSwing)
        {
            this.bladePrefab = bladePrefab;
        }
        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Blade blade = Shooting.Instantiate(bladePrefab, position, Quaternion.identity);
            blade.color = color;
            blade.Launch(shooter, direction.normalized, shooting.weaponDamageMultiplier * baseDamage, projectileSpeed * shooting.projectileSpeedMultiplier);
        }
    }
}
