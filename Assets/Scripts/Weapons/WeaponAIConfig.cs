using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "WeaponAIConfig", menuName = "ScriptableObjects/WeaponAIConfig")]
    public class WeaponAIConfig : ScriptableObject
    {
        public List<WeaponAIProperties> Data = new List<WeaponAIProperties>();

        private Dictionary<WeaponType, WeaponAIProperties> mapping;
        public WeaponAIProperties this[WeaponType type]
        {
            get
            {
                mapping ??= Data.ToDictionary(data => data.Type);
                return mapping[type];
            }
        }
    }

    [Serializable]
    public class WeaponAIProperties
    {
        public WeaponType Type;
        public float MinimumRange;
        public float PreferredRange;
        public float MaximumRange;
        public float TriggerTimeoutMultiplier;
        public float ProjectileSpeedMultiplier;
        public float DamageMultiplier;
        public float MovementSpeedMultiplier;
        public float AmmoRechargeTime;
        public float AmmoRechargeAmmount;
    }
}