using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenuPrefab;
    private GameObject pauseMenuView;
    public Button PauseButton;
    
    public void OpenPauseMenu()
    {
        Debug.Log("pause menu opened");
        pauseMenuView = Instantiate(PauseMenuPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        var pauseMenu = pauseMenuView.transform.GetChild(0).GetChild(0).GetComponent<GameObject>();
        pauseMenu.SetActive(true);
    }
}
