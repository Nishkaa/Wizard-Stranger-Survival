using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject ShootingPoint;
    public float shootAiSpeed = 2000;
    public float fireRate = 20F;
    private float nextFire = 0.0F;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttaked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        //shooting bullets
     
        //Check sight attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }
    public void ShootAiBullet()
    {
        GameObject AiObject;
        //Instantiate/Create Bullet
        AiObject = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        AiObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, shootAiSpeed));
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 10f, whatIsGround))
            walkPointSet = true; 

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position); 
    }
    private void AttackPlayer()
    {
        //Attacks
        //shooting bullets
        if ( Time.time > nextFire)
        {
            //fire rate
            nextFire = Time.time + fireRate;
            ShootAiBullet();
        }
        

        //Not to move
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttaked)
        {
            alreadyAttaked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttaked = false;
    }
}
