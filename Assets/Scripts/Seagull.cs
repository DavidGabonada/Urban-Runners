// using UnityEngine;

// public class Seagull : MonoBehaviour
// {
//     public float speed = 5f; // Flight speed of the seagull
//     public float range = 10f; // The range within which the seagull deals damage
//     public float damage = 10f; // The damage the seagull will deal to the player
//     public Transform player; // Reference to the player's transform
//     public LayerMask playerLayer; // LayerMask for detecting the player

//     private Vector3 startPos;
//     private Vector3 targetPos;
//     private bool isAttacking = false;

//     void Start()
//     {
//         startPos = transform.position;
//         SetTargetPosition(); // Initial position
//     }

//     void Update()
//     {
//         if (!isAttacking)
//         {
//             FlyCinematically(); // Move the seagull along a flight path
//             CheckForPlayer();
//         }
//     }

//     // Simulate cinematic flying
//     void FlyCinematically()
//     {
//         float step = speed * Time.deltaTime;
//         transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

//         if (Vector3.Distance(transform.position, targetPos) < 0.2f)
//         {
//             SetTargetPosition(); // Change the target position once the seagull reaches it
//         }
//     }

//     void SetTargetPosition()
//     {
//         // Calculate a random target within a certain range
//         targetPos = startPos + new Vector3(
//             Random.Range(-range, range),
//             Random.Range(-range, range),
//             Random.Range(-range, range)
//         );
//     }

//     // void CheckForPlayer()
//     // {
//     //     // Detect if the seagull is within a certain range of the player
//     //     if (Vector3.Distance(transform.position, player.position) <= range)
//     //     {
//     //         // Deal damage if close enough to the player
//     //         Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, playerLayer);
//     //         foreach (var hitCollider in hitColliders)
//     //         {
//     //             if (hitCollider.CompareTag("player"))
//     //             {
//     //                 DealDamage(hitCollider.gameObject);
//     //             }
//     //         }
//     //     }
//     // }

//     // void DealDamage(GameObject playerObj)
//     // {
//     //     // Assuming the player has a script with a TakeDamage method
//     //     PlayerHealth playerHealth = playerObj.GetComponent<PlayerHealth>();
//     //     if (playerHealth != null)
//     //     {
//     //         playerHealth.TakeDamage(damage);
//     //     }
//     // }
// }
