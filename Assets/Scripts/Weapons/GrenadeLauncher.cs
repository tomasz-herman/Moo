using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class GrenadeLauncher : Weapon
    {
        private readonly Grenade grenadePrefab;
        private Color color = Color.magenta;

        public override float projectileSpeed { get; set; } = 2f;
        public override float triggerTimeout { get; set; } = 7f;
        public override float baseDamage { get; set; } = 10f;
        public override string Name { get; set; } = "GrenadeLauncher";
        protected override int ammoConsumption => 7;

        public GrenadeLauncher(Grenade grenadeprefab)
        {
            grenadePrefab = grenadeprefab;
        }
        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Grenade grenade = Shooting.Instantiate(grenadePrefab, position, Quaternion.identity);
            grenade.color = color;
            grenade.Launch(shooter, direction.normalized * projectileSpeed * shooting.projectileSpeed, shooting.weaponDamage * baseDamage);
        }
    }
}
