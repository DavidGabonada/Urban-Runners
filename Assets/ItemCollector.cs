using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemCounterText;
    [SerializeField] private ParticleSystem collectEffect; // Particle system for collection effect
    [SerializeField] private AudioSource stageCompleteSound; // Sound for stage completion
    private int itemsCollected = 0;
    [SerializeField] private int totalItems = 9;

    void Start()
    {
        if (itemCounterText == null)
        {
            Debug.LogError("ItemCounterText is not assigned! Please assign it in the Inspector.");
            return;
        }

        UpdateItemCounter();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            // Play particle effect
            if (collectEffect != null)
            {
                Instantiate(collectEffect, other.transform.position, Quaternion.identity);
            }

            itemsCollected++;

            // Destroy the collected object
            Destroy(other.gameObject);

            // Update the UI counter
            if (itemCounterText != null)
            {
                UpdateItemCounter();
            }
            else
            {
                Debug.LogError("itemCounterText is null! Unable to update the counter.");
            }
        }

        // Check if collided with the FinishLine
        if (other.CompareTag("FinishLine"))
        {
            Debug.Log("Stage Complete!");
            StageComplete();
        }
    }

    private void UpdateItemCounter()
    {
        if (itemCounterText != null)
        {
            itemCounterText.text = "Collected: " + itemsCollected + "/" + totalItems;

            // Briefly scale up and down the text for effect
            StartCoroutine(AnimateText());
        }
    }

    private IEnumerator AnimateText()
    {
        Vector3 originalScale = itemCounterText.transform.localScale;
        itemCounterText.transform.localScale = originalScale * 1.2f; // Scale up
        yield return new WaitForSeconds(0.1f);
        itemCounterText.transform.localScale = originalScale; // Scale back to normal
    }

    private void StageComplete()
    {
        Debug.Log("You reached the finish line! Proceed to the next level.");

        // Play stage completion sound
        if (stageCompleteSound != null)
        {
            stageCompleteSound.Play();
        }

        // Load the next scene after a short delay to ensure the sound plays
        StartCoroutine(LoadNextStage());
    }

    private IEnumerator LoadNextStage()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second (adjust if needed)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage 2");
    }
}
