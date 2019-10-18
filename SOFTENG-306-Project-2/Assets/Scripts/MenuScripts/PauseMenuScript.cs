using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenuPrefab;
    private GameObject pauseMenuView;
    public Button PauseButton;
    private GameObject pauseMenu;
    
    public void OpenPauseMenu()
    {
        Time.timeScale = 0f;
        Debug.Log("pause menu opened");
        pauseMenuView = Instantiate(PauseMenuPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        PauseButton.gameObject.SetActive(false);
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("pause menu closed");
        pauseMenu.SetActive(false);
        PauseButton.gameObject.SetActive(true);
    }
    
    public void NavigateToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
