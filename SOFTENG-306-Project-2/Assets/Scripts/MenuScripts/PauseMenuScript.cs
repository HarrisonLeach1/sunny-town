using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
	public GameObject PausePanel;
    public GameObject PauseMenuPrefab;
    private GameObject pauseMenuView;
    public Button PauseButton;
    private GameObject pauseMenu;
	public GameObject SettingsMenuPrefab;
	private GameObject settingsMenuView;
    
    public void OpenPauseMenu()
    {
        Time.timeScale = 0f;
        Debug.Log("pause menu opened");
		PausePanel.gameObject.SetActive(true);
		PauseMenuPrefab.gameObject.SetActive(true);
        //pauseMenuView = Instantiate(PauseMenuPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        PauseButton.gameObject.SetActive(false);
		//settingsMenuView = Instantiate(SettingsMenuPrefab, new Vector3(0,0,0), Quaternion.identity);
		SettingsMenuPrefab.gameObject.SetActive(false);
    }

    public void ClosePauseMenu()
    {
		PausePanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("pause menu closed");
        PauseMenuPrefab.gameObject.SetActive(false);
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

	public void OpenSettings() 
	{
		PauseMenuPrefab.gameObject.SetActive(false);
		SettingsMenuPrefab.gameObject.SetActive(true);
	}

	public void CloseSettings()
	{
		SettingsMenuPrefab.gameObject.SetActive(false);
		PauseMenuPrefab.gameObject.SetActive(true);
	}

}
