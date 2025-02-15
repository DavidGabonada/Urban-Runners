using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScooterCollector : MonoBehaviour
{
    [SerializeField] private ParticleSystem collectEffect; // Particle system for collection effect
    [SerializeField] private AudioSource collectSound; // Sound effect for collecting items
    [SerializeField] private GameObject kickboard; // Reference to the kickboard
    private bool canUseKickboard = false; // Track if the player can use the kickboard
    private bool isKickboard = false; // Track if kickboard mode is active
    [SerializeField] private Animator anim; // Animator reference
    [SerializeField] private float kickboardDuration = 6f; // Duration for kickboard activation

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CollectibleItem"))
        {
            // Enable the kickboard for a limited time
            canUseKickboard = true;
            if (kickboard != null)
            {
                kickboard.SetActive(true);
            }

            // Play particle effect
            if (collectEffect != null)
            {
                Instantiate(collectEffect, other.transform.position, Quaternion.identity);
            }

            // Play sound effect
            if (collectSound != null)
            {
                collectSound.Play();
            }

            // Destroy the collected object
            Destroy(other.gameObject);

            // Start the kickboard timer
            StartCoroutine(KickboardTimer());
        }
    }

    private IEnumerator KickboardTimer()
    {
        yield return new WaitForSeconds(kickboardDuration);
        canUseKickboard = false;
        if (kickboard != null)
        {
            kickboard.SetActive(false);
        }
    }

    public bool CanUseKickboard()
    {
        return canUseKickboard;
    }

    private void Update()
    {
        KickBoard();
    }

    private void KickBoard()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && canUseKickboard)
        {
            isKickboard = !isKickboard;
            anim.SetBool("isKickBoard", isKickboard);
            Debug.Log("KickBoard mode: " + isKickboard);
        }
    }
}
