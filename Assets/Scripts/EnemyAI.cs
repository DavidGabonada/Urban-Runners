// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyAI : MonoBehaviour
// {
//     [SerializeField] private Transform player;
//     private Rigidbody2D enemyRB;
//     [SerializeField] private float moveSpeed = 2.0f; // Adjusted speed to be positive
//     [SerializeField] private AudioSource enemyAudio;

//     private bool willFollow = false;

//     void Start()
//     {
//         enemyRB = GetComponent<Rigidbody2D>();

//         if (enemyAudio == null)
//         {
//             Debug.LogWarning("Enemy AudioSource not assigned!");
//         }
//     }

//     void Update()
//     {
//         if (player == null)
//         {
//             Debug.LogWarning("Player reference is missing!");
//             return;
//         }

//         if (willFollow)
//         {
//             FollowPlayer();
//         }
//     }

//     void FollowPlayer()
//     {
//         Vector2 direction = (player.position - transform.position).normalized;
//         enemyRB.velocity = new Vector2(direction.x * moveSpeed, enemyRB.velocity.y);

//         FacePlayer();
//     }

//     void FacePlayer()
//     {
//         if (player.position.x < transform.position.x)
//         {
//             transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
//         }
//         else
//         {
//             transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.gameObject.CompareTag("Player"))
//         {
//             willFollow = true;
//             FacePlayer(); // Ensure enemy faces the player upon detection
//         }
//     }

//     private void OnTriggerExit2D(Collider2D collision)
//     {
//         if (collision.gameObject.CompareTag("Player"))
//         {
//             willFollow = false;
//             enemyRB.velocity = Vector2.zero; // Stop movement when player leaves
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";  // Tag for the player
    private Transform player;
    private Rigidbody2D enemyRB;
    private SpriteRenderer spriteRenderer;  // Reference to the sprite renderer
    [SerializeField] private float moveSpeed = 10000.0f;  // Adjust speed for better control
    [SerializeField] private float attackRange = 5.0f;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer

        // Disable gravity so the enemy stays in the air
        enemyRB.gravityScale = 0;

        // Find the player by its tag
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player not found with tag: " + playerTag);
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // If within attack range, attack the player
        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Calculate direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move enemy using Rigidbody2D velocity
        enemyRB.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

        // Flip the sprite based on the player's position
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true;  // Facing right
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = false; // Facing left (default)
        }
    }


    void Attack()
    {
        // Attack logic goes here (e.g., trigger animation, deal damage, etc.)
        Debug.Log("Attacking player!");
    }
}
