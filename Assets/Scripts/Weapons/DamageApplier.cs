﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class DamageApplier : MonoBehaviour
    {
        protected GameObject owner;
        public float timeToLive = 10f;
        private float elapsedTime = 0f;

        protected virtual void Update()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > timeToLive)
                Destroy(gameObject);
        }

        public void ApplyDamage(Collider other)
        {
            Enemy enemyHit = other.gameObject.GetComponent<Enemy>();
            Player playerHit = other.gameObject.GetComponent<Player>();
            if (owner != null && owner.GetComponent<Player>() != null) // Player was shooting
            {
                if (enemyHit != null)
                {
                    enemyHit.TakeDamage(15, owner.GetComponent<ScoreSystem>());
                }
            }
            if (owner == null || (owner != null && owner.GetComponent<Enemy>() != null)) // Enemy was shooting (if it is null it means it is dead enemy)
            {
                if (playerHit != null)
                {
                    playerHit.healthSystem.Health -= 10;
                }
            }
        }

    }
}
