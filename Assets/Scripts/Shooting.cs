using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Projectile projectilePrefab;
    public float projectileSpeed = 3f;
    public float triggerTimeout = 0.5f;

    private ContinuousTrigger trigger = new ContinuousTrigger();
    public AmmoSystem ammoSystem;

    void Start()
    {
        
    }

    void Update()
    {
        trigger.DecreaseTime(Time.deltaTime);
    }

    public void TryShoot(GameObject shooter, Vector3 position, Vector3 direction)
    {
        if (ammoSystem.Ammo == 0)
            return;
        int dischargeCount = trigger.PullTrigger(triggerTimeout);
        if (projectilePrefab != null)
        {
            for(int i = 0; i < dischargeCount; i++)
            {
                if(ammoSystem.Ammo > 0)
                {
                    Projectile projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
                    projectile.Launch(shooter, direction.normalized * projectileSpeed);
                    ammoSystem.Ammo--;

                    FindObjectOfType<AudioManager>()?.Play(Assets.Scripts.SoundManager.SoundType.LaserShot);
                }
            }
        }
    }
}
