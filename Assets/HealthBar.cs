using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth; // Set the initial value to max health
    }

    public void SetHealth(int currentHealth)
    {
        healthSlider.value = currentHealth; // Update health bar
    }
}
