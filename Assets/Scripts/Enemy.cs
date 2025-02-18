using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    // Call this method to apply damage to the enemy
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);  // Destroy the enemy object
    }
}
