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
        Debug.Log("pause menu opened");
        pauseMenuView = Instantiate(PauseMenuPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        pauseMenu = pauseMenuView.transform.GetChild(0).GetChild(0).GetComponent<GameObject>();
        pauseMenu.SetActive(true);
        PauseButton.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        Debug.Log("pause menu opened");
        pauseMenu.SetActive(false);
        PauseButton.gameObject.SetActive(true);
        Time.timeScale = 1;
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
