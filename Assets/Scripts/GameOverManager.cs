using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class G : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadMainMenu());
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(2f);
        GoBacktoMenu();
    }

    void GoBacktoMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
