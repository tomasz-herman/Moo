using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Sword : Weapon
    {
        private readonly Blade bladePrefab;
        private Color color = Color.green;


        public override float projectileSpeed { get; set; } = 2f;
        public override float triggerTimeout { get; set; } = 2f;
        public override float baseDamage { get; set; } = 1f;

        public override string Name { get; set; } = "Sword";
        protected override int ammoConsumption => 0;

        public Sword(Blade bladeprefab)
        {
            bladePrefab = bladeprefab;
        }
        public override void Shoot(GameObject shooter, Vector3 position, Vector3 direction, Shooting shooting)
        {
            Blade blade = Shooting.Instantiate(bladePrefab, position, Quaternion.identity);
            blade.color = color;
            blade.Launch(shooter, direction.normalized, shooting.weaponDamage * baseDamage, projectileSpeed * shooting.projectileSpeed);
        }
    }
}
