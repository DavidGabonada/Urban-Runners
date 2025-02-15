using UnityEngine;
using UnityEngine.AI;

public class HomelessAI : MonoBehaviour
{
    public Transform player;                // Reference to the player
    public float attackRange = 1.5f;        // Distance for melee attacks
    public float throwRange = 4f;           // Distance for throwing projectiles
    public float moveSpeed = 3.5f;          // Speed of the AI movement
    public GameObject projectilePrefab;     // Prefab for the projectile
    public Transform throwPoint;            // Location where the projectile is spawned
    public float throwCooldown = 3f;        // Time between throws
    public float attackCooldown = 1.5f;     // Time between melee attacks

    private NavMeshAgent agent;             // NavMeshAgent for pathfinding
    private float throwTimer = 0f;          // Timer for managing projectile cooldown
    private float attackTimer = 0f;         // Timer for managing melee attack cooldown

    void Start()
    {
        // Initialize the NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = moveSpeed;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Handle movement
        if (distanceToPlayer > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
            if (attackTimer <= 0f)
            {
                Attack();
                attackTimer = attackCooldown; // Reset melee attack cooldown
            }
        }

        // Handle projectile throwing
        if (distanceToPlayer <= throwRange && distanceToPlayer > attackRange && throwTimer <= 0f)
        {
            ThrowProjectile();
            throwTimer = throwCooldown; // Reset projectile cooldown
        }

        // Cooldown timers
        if (throwTimer > 0f) throwTimer -= Time.deltaTime;
        if (attackTimer > 0f) attackTimer -= Time.deltaTime;
    }

    private void Attack()
    {
        Debug.Log("HomelessAI is attacking the player!");
        // Add melee attack logic here (e.g., reduce player health)
    }

    private void ThrowProjectile()
    {
        Debug.Log("HomelessAI is throwing a projectile!");

        if (projectilePrefab != null && throwPoint != null)
        {
            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, throwPoint.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = (player.position - throwPoint.position).normalized;
                rb.AddForce(direction * 10f, ForceMode.Impulse); // Adjust force as needed
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Draw the throw range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, throwRange);
    }
}
