using System;
using Assets.Scripts.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleEnemyAI : MonoBehaviour
{
    private CharacterController characterController;
    
    private Transform player;
    
    public float sightRange;
    private bool playerInAttackRange, playerInSight, playerInPrefferedRange;
    
    public Vector3 movementDirection;
    public float remainingMovementTime = 0;
    
    private Shooting shooting;
    public AmmoSystem ammoSystem;
    protected HealthSystem healthSystem;
    public WeaponAI weaponAI;
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
        weaponAIProperties = WeaponAIProperties.Get(weaponAI);
        lastPlayerPosition = player.position;
    }

    protected void Update()
    {
        shooting.SelectWeapon(weaponAIProperties.Type);
        //TODO weaponAIProperties.Timeout see below
        //TODO weaponAIProperties.ProjectileSpeed see below
        //TODO weaponAIProperties.BonusDamage is unused, make sure it has no purpose before deleting this TODO

        var position = transform.position;
        var playerPosition = player.position;
        var distToPlayer = Vector3.Distance(position, playerPosition);
        playerInAttackRange = distToPlayer < weaponAIProperties.MaximumRange;
        playerInPrefferedRange = distToPlayer < weaponAIProperties.PreferredRange;
        playerInSight = distToPlayer < sightRange;

        if(!playerInSight && !playerInAttackRange) Patrol();
        if(playerInSight && !playerInPrefferedRange) Chase();
        if(playerInSight && playerInAttackRange) Attack();
        lastPlayerPosition = playerPosition;
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
        
        characterController.Move(movementDirection * Time.deltaTime * enemy.movementSpeed);
        remainingMovementTime -= Time.deltaTime;
    }

    private void Chase()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        characterController.Move(toPlayer * (Time.deltaTime * enemy.movementSpeed * weaponAIProperties.BonusMovementSpeed));
    }

    private void Attack()
    {
        var position = transform.position;
        var playerPosition = player.position;
        var playerVelocity = (playerPosition - lastPlayerPosition) *  (Utils.FloatBetween(0, 2) / Time.deltaTime);
        Vector3 toPlayer = (playerPosition + playerVelocity - position).normalized;
        shooting.TryShoot(gameObject, position + new Vector3(0, 1, 0), toPlayer);
    }
}