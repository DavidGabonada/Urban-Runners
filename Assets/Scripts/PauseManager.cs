using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        // Press "P" to toggle pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause game time
            AudioListener.pause = true; // Pause all audio
        }
        else
        {
            Time.timeScale = 1f; // Resume game time
            AudioListener.pause = false; // Resume audio
        }
    }
}
