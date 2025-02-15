using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth = 100f;  // Set your starting health value in the inspector
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // Player is still alive, you can implement healing or other effects here
            Debug.Log("Player is alive with " + currentHealth + " health.");
        }
        else
        {
            // Player is dead, trigger death behavior here
            Debug.Log("Player is dead.");
            Die(); // Call a method for death behavior (e.g., play animation, respawn, etc.)
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);  // Test damage when the "E" key is pressed
        }
    }

    private void Die()
    {
        // Implement your death logic, such as disabling the player, showing game over, etc.
        Debug.Log("Death triggered. Implement respawn or game over logic.");
    }
}
