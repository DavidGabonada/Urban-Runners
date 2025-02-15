using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarAI : MonoBehaviour
{
    Rigidbody2D enemyRB; // Rigidbody for movement
    float moveSpeed = 8.0f; // Speed of the enemy

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        // Ensure the car is facing left at the start
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
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
        if (collision.CompareTag("PlayerAttack")) // Player's attack
        {
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("stop")) // STOP object
        {
            Destroy(this.gameObject);
        }
    }


    // Detect collisions with the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Assuming the player has the tag "Player"
        {
            SceneManager.LoadScene("GameOver"); // Load the Game Over scene
        }
    }
}