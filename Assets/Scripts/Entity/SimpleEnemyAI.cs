using System;
using Assets.Scripts.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleEnemyAI : MonoBehaviour
{
    private CharacterController characterController;
    
    private Transform player;
    
    public float sightRange;
    private bool playerInAttackRange, playerInSight;
    
    public float movementSpeed = 1f;
    public Vector3 movementDirection;
    public float remainingMovementTime = 0;
    
    private Shooting shooting;
    public AmmoSystem ammoSystem;
    protected HealthSystem healthSystem;
    public WeaponAI weaponAI;
    protected WeaponAIProperties weaponAIProperties;

    protected void Start()
    {
        characterController = GetComponent<CharacterController>();
        healthSystem = GetComponent<HealthSystem>();
        player = GameObject.Find("Player").transform;
        shooting = GetComponent<Shooting>();
        ammoSystem = GetComponent<AmmoSystem>();
        shooting.ammoSystem = ammoSystem;
        weaponAIProperties = WeaponAIProperties.Get(weaponAI);
    }

    protected void Update()
    {
        shooting.SelectWeapon(weaponAIProperties.Type);
        shooting.triggerTimeout = weaponAIProperties.Timeout;

        var position = transform.position;
        var playerPosition = player.position;
        playerInAttackRange = Vector3.Distance(position, playerPosition) < weaponAIProperties.Range;
        playerInSight = Vector3.Distance(position, playerPosition) < sightRange;

        if(!playerInSight && !playerInAttackRange) Patrol();
        if(playerInSight && !playerInAttackRange) Chase();
        if(playerInSight && playerInAttackRange) Attack();
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
        
        characterController.Move(movementDirection * Time.deltaTime * movementSpeed);
        remainingMovementTime -= Time.deltaTime;
    }

    private void Chase()
    {
        Vector3 toPlayer = (player.position - transform.position).normalized;
        characterController.Move(toPlayer * Time.deltaTime * movementSpeed);
    }

    private void Attack()
    {
        var position = transform.position;
        Vector3 toPlayer = (player.position - position).normalized;
        characterController.Move(toPlayer * Time.deltaTime * movementSpeed);
        shooting.TryShoot(gameObject, position + new Vector3(0, 1, 0), toPlayer);
    }
}