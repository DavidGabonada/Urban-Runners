using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClearSky; // Add this line to reference the Player class in the ClearSky namespace

public class EnemyDamage : MonoBehaviour
{
    public int damage;   // Damage the enemy will apply

    private Player player; // Reference to the player

    void Start()
    {
        // Cache the player reference when the script starts
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the player
        if (collision.CompareTag("Player") && player != null)
        {
            // Apply damage to the player
            player.TakeDamage(damage);
            Debug.Log("Damage dealt: " + damage);
        }
    }
}
