using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenuScript : MonoBehaviour
{
    public void NavigateToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("WorldScene");
    }
}
