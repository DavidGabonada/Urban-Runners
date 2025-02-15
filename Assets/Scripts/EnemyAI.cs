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
    [SerializeField] Transform player;
    private Rigidbody2D enemyRB;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float attackRange = 5.0f;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            Debug.LogWarning("Player reference is missing!");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Flip the enemy to face the player without distorting its size
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        }

        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }
}

