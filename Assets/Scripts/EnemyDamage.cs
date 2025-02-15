using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClearSky; // Add this line to reference the Player class in the ClearSky namespace

public class EnemyDamage : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = FindObjectOfType<Player>();
            player.TakeDamage(damage);
            Debug.Log("Damage:  " + damage);
        }
    }

}
