using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarAI2 : MonoBehaviour
{
    Rigidbody2D enemyRB; // Rigidbody for movement
    float moveSpeed = 20.0f; // Speed of the enemy

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        // Ensure the car is facing left at the start
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        // Ignore collisions with obstacles
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Car"), LayerMask.NameToLayer("Obstacle"), true);
    }

    void Update()
    {
        // Move the car straightforward along the X-axis
        MoveStraight();
    }

    void MoveStraight()
    {
        // Move the car to the left (negative X direction)
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    // Detect collisions with the player's attack
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack")) // Assuming the player's attack has the tag "PlayerAttack"
        {
            Destroy(this.gameObject); // Destroy the enemy immediately
        }
    }

    // Detect collisions with the player or Stop tagged object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Check if collided with the player
        {
            SceneManager.LoadScene("GameOver"); // Load the Game Over scene
        }
        else if (collision.gameObject.CompareTag("stop")) // Check if collided with a Stop tagged object
        {
            Destroy(this.gameObject); // Destroy the car
        }
    }
}
