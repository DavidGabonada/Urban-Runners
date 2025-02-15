using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalStage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemCounterText;
    [SerializeField] private ParticleSystem collectEffect; // Particle system for collection effect
    [SerializeField] private AudioSource stageCompleteSound; // Sound for stage completion
    private int itemsCollected = 0;
    [SerializeField] private int totalItems = 7;

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
            UpdateItemCounter();
        }

        // Check if player reaches the finish line
        if (other.CompareTag("FinishLine"))
        {
            Debug.Log("Stage Complete!");
            Stage4Complete();
        }
    }

    private void UpdateItemCounter()
    {
        if (itemCounterText != null)
        {
            itemCounterText.text = "Coins Collected: " + itemsCollected + "/" + totalItems;

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

    private void Stage4Complete()
    {
        Debug.Log("You");

        // Play stage completion sound
        if (stageCompleteSound != null)
        {
            stageCompleteSound.Play();
        }

        // Load the next scene after a short delay to ensure the sound plays
        StartCoroutine(LoadNextStage4());
    }

    private IEnumerator LoadNextStage4()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage Complete");
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
