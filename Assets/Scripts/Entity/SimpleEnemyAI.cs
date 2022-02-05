using System;
using UnityEngine;
using Weapons;
using Random = UnityEngine.Random;

public class SimpleEnemyAI : MonoBehaviour
{
    private CharacterController characterController;
    
    private Transform player;
    
    public float sightRange;
    private bool playerInAttackRange, playerInSight, playerInPrefferedRange, playerTooClose, outOfAmmo;
    
    public Vector3 movementDirection;
    public float dodgeDirection;
    public float remainingMovementTime = 0;
    public float remainingDodgeTime = 0;
    public float remainingTumbleTime = 0;
    private float remainingAmmoRechargeTime = 0;
    public float remainingLagTime = 0;

    private Shooting shooting;
    public AmmoSystem ammoSystem;
    protected HealthSystem healthSystem;
    public WeaponType weapon;
    protected WeaponAIProperties weaponAIProperties;
    private Vector3 lastPlayerPosition;
    protected Enemy enemy;

    protected void Start()
    {
        characterController = GetComponent<CharacterController>();
        healthSystem = GetComponent<HealthSystem>();
        player = GameObject.Find("Player").transform;
        shooting = GetComponent<Shooting>();
        ammoSystem = GetComponent<AmmoSystem>();
        enemy = GetComponent<Enemy>();
        shooting.ammoSystem = ammoSystem;
        weaponAIProperties = ApplicationData.WeaponAIData[weapon];
        lastPlayerPosition = player.position;
        remainingLagTime = Random.value;
    }

    protected void Update()
    {
        shooting.SelectWeapon(weaponAIProperties.Type);
        shooting.triggerTimeoutMultiplier = enemy.data.BaseTriggerTimeoutMultiplier * weaponAIProperties.TriggerTimeoutMultiplier;
        shooting.weaponDamageMultiplier = enemy.data.BaseDamageMultiplier * weaponAIProperties.DamageMultiplier;
        shooting.projectileSpeedMultiplier = enemy.data.BaseProjectileSpeedMultiplier * weaponAIProperties.ProjectileSpeedMultiplier;

        if (remainingLagTime > 0)
        {
            remainingLagTime -= Time.deltaTime;
            lastPlayerPosition = player.position;
            return;
        }
        
        var position = transform.position;
        var playerPosition = player.position;
        var distToPlayer = Vector3.Distance(position, playerPosition);
        playerInAttackRange = distToPlayer < weaponAIProperties.MaximumRange;
        playerInPrefferedRange = distToPlayer < weaponAIProperties.PreferredRange;
        playerInSight = distToPlayer < sightRange;
        playerTooClose = distToPlayer < weaponAIProperties.MinimumRange;
        outOfAmmo = !shooting.HasEnoughAmmo();

        if (outOfAmmo)
        {
            remainingAmmoRechargeTime -= Time.deltaTime;
            if (remainingAmmoRechargeTime <= 0) Recharge();
            else Dodge();
        }

        if(!playerInSight && !playerInAttackRange) Patrol();
        if(playerInSight && !playerInPrefferedRange) Chase();
        if(playerInSight && playerInAttackRange) Attack();
        if(playerTooClose) Escape(); 
        lastPlayerPosition = playerPosition;
    }

    private void Dodge()
    {
        if (remainingDodgeTime <= 0)
        {
            // Left or right
            dodgeDirection = Utils.RandomBool() ? -1 : 1;

            remainingDodgeTime = Utils.FloatBetween(0.25f, 1.25f);
        }
        
        var position = transform.position;
        var playerPosition = player.position;
        Vector3 toPlayer = (playerPosition - position).normalized;
        // Perpendicular to player
        movementDirection = Vector3.Cross(toPlayer, Vector3.up).normalized * dodgeDirection;
        
        characterController.Move(movementDirection * (Time.deltaTime * enemy.movementSpeed * 2));
        remainingDodgeTime -= Time.deltaTime;
    }

    private void Recharge()
    {
        remainingAmmoRechargeTime += weaponAIProperties.AmmoRechargeTime;
        shooting.ammoSystem.Ammo += weaponAIProperties.AmmoRechargeAmmount;
    }

    private void Patrol()
    {
        if (remainingMovementTime <= 0)
        {
            float randomX = Utils.FloatBetween(-1, 1);
            float randomZ = Utils.FloatBetween(-1, 1);

            movementDirection = new Vector3(randomX, 0, randomZ).normalized;

            remainingMovementTime = Random.Range(2, 8);
        }
        
        characterController.Move(movementDirection * (Time.deltaTime * enemy.movementSpeed));
        remainingMovementTime -= Time.deltaTime;
    }

    private void Chase()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        characterController.Move(toPlayer * (Time.deltaTime * enemy.movementSpeed * weaponAIProperties.MovementSpeedMultiplier));
    }
    
    private void Escape()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        characterController.Move(-toPlayer * (Time.deltaTime * enemy.movementSpeed * weaponAIProperties.MovementSpeedMultiplier));
    }

    private void Attack()
    {
        if (remainingTumbleTime > 0)
        {
            Dodge();
            remainingTumbleTime -= Time.deltaTime;
        }
        if (Random.value < 0.1 * Time.deltaTime) remainingTumbleTime += Utils.FloatBetween(0.25f, 1.25f);
        
        var position = transform.position;
        var playerPosition = player.position;
        var distance = Vector3.Distance(position, playerPosition);
        var playerVelocity = (playerPosition - lastPlayerPosition) * (distance * Utils.RandomGaussNumber(1, 1) / (Time.deltaTime * 25));
        Vector3 toPlayer = (playerPosition + playerVelocity - position).normalized;
        shooting.TryShoot(gameObject, position + new Vector3(0, 1, 0), toPlayer);
    }
}