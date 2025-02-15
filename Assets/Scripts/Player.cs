using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClearSky
{
    public class Player : MonoBehaviour
    {
        public float movePower = 10f;
        public float kickBoardMovePower = 15f;
        public float jumpPower = 20f;

        public int maxHealth = 100;
        public int currentHealth;
        public HealthBar healthBar;
        public float brickSpeed = 20f;

        private Rigidbody2D rb;
        private Animator anim;
        private int direction = 1;
        private int jumpCount = 0;
        private const int maxJumps = 2;
        private bool alive = true;
        private bool isKickboard = false;

        [SerializeField] private GameObject brick;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            Debug.Log("Player Initialized. Health: " + currentHealth);
        }

        private void Update()
        {
            Restart();
            if (alive)
            {
                Jump();
                ToggleKickboard();
                Run();
            }
        }

        public void TakeDamage(int damage)
        {
            if (!alive) return;

            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            healthBar.SetHealth(currentHealth);

            Debug.Log("Player took damage. Current Health: " + currentHealth);
            anim.SetTrigger("hurt");

            rb.AddForce(new Vector2(direction * -5f, 1f), ForceMode2D.Impulse);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            if (!alive) return;

            Debug.Log("Player died.");
            anim.SetTrigger("die");

            alive = false;
            isKickboard = false;
            anim.SetBool("isKickBoard", false);

            StartCoroutine(DisablePhysics());
        }

        IEnumerator DisablePhysics()
        {
            yield return new WaitForSeconds(1f);
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            enabled = false;
            SceneManager.LoadScene("Game Over");
        }

        void ToggleKickboard()
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                isKickboard = !isKickboard;
                anim.SetBool("isKickBoard", isKickboard);
                Debug.Log("KickBoard mode: " + isKickboard);
            }
        }

        void Run()
        {
            float moveInput = Input.GetAxisRaw("Horizontal");

            if (moveInput == 0)
            {
                anim.SetBool("isRun", false);
                return;
            }

            direction = moveInput < 0 ? -1 : 1;
            transform.localScale = new Vector3(direction, 1, 1);

            float speed = isKickboard ? kickBoardMovePower : movePower;
            transform.position += moveInput * speed * Time.deltaTime * Vector3.right;

            if (!anim.GetBool("isJump"))
            {
                anim.SetBool("isRun", true);
            }
        }

        void Jump()
        {
            if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)) && jumpCount < maxJumps)
            {
                Debug.Log("Jump triggered! Jump Count: " + jumpCount);
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                jumpCount++;
                anim.SetBool("isJump", true);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.contacts[0].normal.y > 0.5f)
            {
                Debug.Log("Landed on Ground! Resetting jumps.");
                jumpCount = 0;
                anim.SetBool("isJump", false);
            }

            if (collision.gameObject.CompareTag("Enemy"))
            {
                HandleEnemyCollision(collision);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.CompareTag("brick"))
            {
                Debug.Log("Player hit by brick!");
                TakeDamage(20);
                Destroy(collision.gameObject);
            }
        }

        private void HandleEnemyCollision(Collision2D collision)
        {
            if (rb.velocity.y < 0)
            {
                var enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(50);
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower - 2f);
                }
            }
            else if (isKickboard)
            {
                Debug.Log("Kickboard hit an enemy! Removing kickboard and enemy.");
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
                Destroy(collision.gameObject);
            }
            else
            {
                TakeDamage(20);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("EnemyTrigger"))
            {
                Vector2 brickPosition = other.transform.position + new Vector3(5f, 15f);
                GameObject newBrick = Instantiate(brick, brickPosition, Quaternion.identity);

                Rigidbody2D rb = newBrick.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.down * brickSpeed; // Moves the brick downward
                }

                Debug.Log("Enemy Triggered!");
            }
        }

        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Debug.Log("Game Restarted.");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public class Enemy : MonoBehaviour
    {
        public int health = 100;
        private Animator anim;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
            else
            {
                TriggerHurtAnimation();
            }
        }

        public void TriggerHurtAnimation()
        {
            if (anim != null)
            {
                anim.SetTrigger("hurt");
            }
        }

        void Die()
        {
            if (anim != null)
            {
                anim.SetTrigger("die");
            }
            Destroy(gameObject, 1f);
        }
    }
}