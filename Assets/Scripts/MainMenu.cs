using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this line

public class MainMenu : MonoBehaviour
{
    public void PlayerGame()
    {
        SceneManager.LoadScene("Stage 1"); // Fix typo: SceneManager.GetActive()
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
