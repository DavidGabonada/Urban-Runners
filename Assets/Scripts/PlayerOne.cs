using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import Scene Management

namespace ClearSky
{
    public class PlayerOne : MonoBehaviour
    {
        public float movePower = 10f;
        public float KickBoardMovePower = 15f;
        public float jumpPower = 20f;

        public int currentHealth;
        public int maxHealth = 100;
        public HealthBar healthBar;

        public LayerMask groundLayer; // Add a LayerMask for ground detection

        private Rigidbody2D rb;
        private Animator anim;
        private int direction = 1;
        private bool alive = true;
        private bool isKickboard = true; // Automatically riding the scooter

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);

            // Automatically start on the scooter
            isKickboard = true;
            anim.SetBool("isKickBoard", true);
        }

        private void Update()
        {
            Restart();

            if (alive)
            {
                Jump();
                Run();
            }
        }

        void LateUpdate()
        {
            if (!Input.GetKeyDown(KeyCode.Alpha5) && alive)
            {
                StartCoroutine(ResetKickboard());
            }
        }

        IEnumerator ResetKickboard()
        {
            yield return new WaitForSeconds(0.5f); // Delay before re-enabling kickboard
            isKickboard = true;
            anim.SetBool("isKickBoard", true);
        }

        public void TakeDamage(int damage)
        {
            if (!alive) return;

            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            Hurt(); // Hurt animation and knockback apply

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
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

            Vector3 moveVelocity = moveInput * (isKickboard ? KickBoardMovePower : movePower) * Time.deltaTime * Vector3.right;
            transform.position += moveVelocity;

            if (!anim.GetBool("isJump"))
            {
                anim.SetBool("isRun", true);
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
                rb.velocity = new Vector2(rb.velocity.x, 0);

                float jumpForce = isKickboard ? jumpPower * 1.1f : jumpPower; // Slightly higher jump if on kickboard
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                anim.SetBool("isJump", true);
            }
        }

        bool IsGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        }

        void Hurt()
        {
            anim.SetTrigger("hurt");
            rb.AddForce(new Vector2(direction * -5f, 1f), ForceMode2D.Impulse);
        }

        void Die()
        {
            isKickboard = false;
            anim.SetBool("isKickBoard", false);
            anim.SetTrigger("die");
            alive = false;
        }

        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                isKickboard = true;
                anim.SetBool("isKickBoard", true);
                anim.SetTrigger("idle");
                alive = true;
                currentHealth = maxHealth;
                healthBar.SetMaxHealth(maxHealth);
                transform.position = new Vector3(0, 0, 0);
            }
        }
    }
}
