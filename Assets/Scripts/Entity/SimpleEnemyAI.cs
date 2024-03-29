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
    private float remainingAmmoRechargeTime = 0;
    public float remainingLagTime = 0;

    private Shooting shooting;
    public AmmoSystem ammoSystem;
    protected HealthSystem healthSystem;
    public WeaponType weapon;
    protected WeaponAIProperties weaponAIProperties;
    private Vector3 lastPlayerPosition;
    protected Enemy enemy;
    
    private float TotalMovementSpeed => enemy.movementSpeed * weaponAIProperties.MovementSpeedMultiplier;

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

        foreach(WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
        {
            var weapon = shooting[weaponType];
            var properties = ApplicationData.WeaponAIData[weaponType];
            weapon.basetriggerTimeout *= enemy.data.BaseTriggerTimeoutMultiplier * properties.TriggerTimeoutMultiplier;
            weapon.baseDamage *= enemy.data.BaseDamageMultiplier * properties.DamageMultiplier;
            weapon.baseProjectileSpeed *= enemy.data.BaseProjectileSpeedMultiplier * properties.ProjectileSpeedMultiplier;
        }
    }

    protected void Update()
    {
        if (enemy.isDead)
            return;

        var weaponType = weaponAIProperties.Type;
        shooting.SelectWeapon(weaponType);

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

        if (!playerInSight && !playerInAttackRange) Patrol();
        if (playerInSight && !playerInPrefferedRange) Chase();
        if (playerInSight && playerInAttackRange) Attack();
        if (playerTooClose) Escape();
        lastPlayerPosition = playerPosition;
        movementDirection = characterController.velocity.normalized;
        transform.LookAt(playerPosition, Vector3.up);
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
        
        characterController.Move(movementDirection * (Time.deltaTime * TotalMovementSpeed));
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
        
        characterController.Move(movementDirection * (Time.deltaTime * TotalMovementSpeed));
        remainingMovementTime -= Time.deltaTime;
    }

    private void Chase()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        characterController.Move(toPlayer * (Time.deltaTime * TotalMovementSpeed));
    }
    
    private void Escape()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        characterController.Move(-toPlayer * (Time.deltaTime * TotalMovementSpeed));
    }

    private void Attack()
    {
        var position = transform.position;
        var playerPosition = player.position;
        var distance = Vector3.Distance(position, playerPosition);
        var aimingDirection = (playerPosition - lastPlayerPosition) * (distance * Utils.RandomGaussNumber(1, 1) / (Time.deltaTime * 25));
        Vector3 toPlayer = (playerPosition + aimingDirection - position).normalized;
        transform.LookAt(player, Vector3.up);
        shooting.TryShoot(gameObject, position + new Vector3(0, 1, 0) + toPlayer, toPlayer);
    }
}