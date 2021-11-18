using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Projectile projectilePrefab;
    public float projectileSpeed = 3f;
    public float triggerTimeout = 0.5f;

    private ContinuousTrigger trigger = new ContinuousTrigger();

    void Start()
    {
        
    }

    void Update()
    {
        trigger.DecreaseTime(Time.deltaTime);
    }

    public void TryShoot(GameObject shooter, Vector3 position, Vector3 direction)
    {
        int dischargeCount = trigger.PullTrigger(triggerTimeout);
        if (projectilePrefab != null)
        {
            for(int i = 0; i < dischargeCount; i++)
            {
                Projectile projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
                projectile.Launch(shooter, direction.normalized * projectileSpeed);

                FindObjectOfType<AudioManager>()?.Play(Assets.Scripts.SoundManager.SoundType.LaserShot);
            }
        }
    }
}
