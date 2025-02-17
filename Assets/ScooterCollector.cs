using System.Collections;
using UnityEngine;

public class ScooterCollector : MonoBehaviour
{
    [SerializeField] private ParticleSystem collectEffect;
    [SerializeField] private AudioSource collectSound;
    [SerializeField] private GameObject kickboard;
    [SerializeField] private Animator anim;
    [SerializeField] private float kickboardDuration = 6f;

    private bool canActivateKickboard = false; // Ensure it's false initially
    private bool isKickboardActive = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CollectibleItem"))
        {
            // Enable kickboard activation
            canActivateKickboard = true;

            // Play particle effect
            if (collectEffect != null)
                Instantiate(collectEffect, other.transform.position, Quaternion.identity);

            // Play sound effect
            collectSound?.Play();

            // Destroy the collected object
            Destroy(other.gameObject);

            Debug.Log("Collected item! Kickboard ready. Press Alpha4 to activate.");
        }
    }

    private IEnumerator KickboardTimer()
    {
        yield return new WaitForSeconds(kickboardDuration);
        DeactivateKickboard();
        canActivateKickboard = false; // Force reset again
    }


    private void ActivateKickboard()
    {
        if (!canActivateKickboard) return; // Double-check activation status

        isKickboardActive = true;
        anim.SetBool("isKickBoard", true);
        kickboard?.SetActive(true);
        Debug.Log("Kickboard activated!");

        StartCoroutine(KickboardTimer());
    }

    private void DeactivateKickboard()
    {
        isKickboardActive = false;
        canActivateKickboard = false; // Reset for next collection
        anim.SetBool("isKickBoard", false);
        kickboard?.SetActive(false);
        Debug.Log("Kickboard deactivated! Collect another item to use again.");
    }

    private void Update()
    {
        Debug.Log($"canActivateKickboard: {canActivateKickboard} | isKickboardActive: {isKickboardActive}");

        // Allow activation with either L or Alpha4
        if ((Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Alpha4)) && canActivateKickboard && !isKickboardActive)
        {
            ActivateKickboard();
        }
    }

    private void OnEnable()
    {
        // Ensure it's disabled on start
        canActivateKickboard = false;
        isKickboardActive = false;
    }

}
