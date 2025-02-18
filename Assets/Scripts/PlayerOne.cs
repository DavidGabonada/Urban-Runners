using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClearSky
{
    public class PlayerOne : MonoBehaviour
    {
        public enum CharacterType { Player, Enemy }

        public CharacterType characterType = CharacterType.Player;

        public float movePower = 10f;
        public float jumpPower = 20f;

        public int currentHealth;
        public int maxHealth = 100;
        public HealthBar healthBar;

        public LayerMask groundLayer;

        private Rigidbody2D rb;
        private Animator anim;
        private int direction = 1;
        private bool alive = true;
        public int damage = 10;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        private void Update()
        {
            Restart();

            if (alive)
            {
                if (characterType == CharacterType.Player)
                {
                    Jump();
                    Run();
                }
            }
        }

        public void TakeDamage(int damage)
        {
            if (!alive) return;

            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if (characterType == CharacterType.Player)
            {
                Hurt(); // Hurt animation and knockback for player
            }
            else if (characterType == CharacterType.Enemy)
            {
                TriggerHurtAnimation(); // Hurt animation for enemy
            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }

        void Run()
        {
            if (characterType == CharacterType.Player)
            {
                float moveInput = Input.GetAxisRaw("Horizontal");

                if (moveInput == 0)
                {
                    anim.SetBool("isRun", false);
                    return;
                }

                direction = moveInput < 0 ? -1 : 1;
                transform.localScale = new Vector3(direction, 1, 1);

                Vector3 moveVelocity = moveInput * movePower * Time.deltaTime * Vector3.right;
                transform.position += moveVelocity;

                if (!anim.GetBool("isJump"))
                {
                    anim.SetBool("isRun", true);
                }
            }
        }

        void Jump()
        {
            if (IsGrounded())
            {
                anim.SetBool("isJump", false);
            }

            if (Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
            {
                if (IsGrounded())
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    anim.SetBool("isJump", true);
                }
            }
        }

        bool IsGrounded()
        {
            bool grounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
            Debug.Log("Is Grounded: " + grounded); // Add this for debugging
            return grounded;
        }


        void Hurt()
        {
            anim.SetTrigger("hurt");
            rb.AddForce(new Vector2(direction * -5f, 1f), ForceMode2D.Impulse);
        }

        void TriggerHurtAnimation()
        {
            anim.SetTrigger("hurt");
            rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
        }

        void Die()
        {
            anim.SetTrigger("die");
            alive = false;

            StartCoroutine(RespawnAfterDelay());
        }

        IEnumerator RespawnAfterDelay()
        {
            yield return new WaitForSeconds(2f);
            if (characterType == CharacterType.Player)
            {
                SceneManager.LoadScene("Game Over");
            }
            else
            {
                StartCoroutine(DisablePhysics());
            }
        }

        IEnumerator DisablePhysics()
        {
            yield return new WaitForSeconds(1f);
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            enabled = false;
            Destroy(gameObject);
        }

        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                anim.SetTrigger("idle");
                alive = true;
                currentHealth = maxHealth;
                healthBar.SetMaxHealth(maxHealth);
                transform.position = new Vector3(0, 0, 0);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (characterType == CharacterType.Enemy && collision.gameObject.CompareTag("Player"))
            {
                PlayerOne player = collision.gameObject.GetComponent<PlayerOne>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
            }
        }
    }
}
