using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour
{
    private CharacterController characterController;
    
    private Transform player;
    
    public float attackRange;
    public float sightRange;
    private bool playerInAttackRange, playerInSight;
    
    public float movementSpeed = 1f;
    public Vector3 movementDirection;
    public float remainingMovementTime = 0;
    
    private Shooting shooting;
    public AmmoSystem ammoSystem;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        player = GameObject.Find("Player").transform;
        shooting = GetComponent<Shooting>();
        ammoSystem = GetComponent<AmmoSystem>();
        shooting.ammoSystem = ammoSystem;
    }

    private void Update()
    {
        var position = transform.position;
        playerInAttackRange = Vector3.Distance(position, player.position) < attackRange;
        playerInSight = Vector3.Distance(position, player.position) < sightRange;

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
        Vector3 toPlayer = (player.position - transform.position).normalized;
        characterController.Move(toPlayer * Time.deltaTime * movementSpeed * 0.1f);
        shooting.TryShoot(gameObject, transform.position + new Vector3(0, 1, 0), toPlayer);
    }
}
