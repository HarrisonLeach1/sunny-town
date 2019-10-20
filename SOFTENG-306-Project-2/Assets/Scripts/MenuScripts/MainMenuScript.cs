using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SunnyTown
{
    /// <summary>
    /// The MainMenuScript is a simple script used to load the Main Menu of the game
    /// </summary>
    public class MainMenuScript : MonoBehaviour
    {
        public GameObject PlayerInput;
        public void PlayGame()
        {
            Time.timeScale = 1f;
            Debug.Log("Start game");
            AchievementsManager.playerName = PlayerInput.GetComponent<TMP_InputField>().text;
            Debug.Log("Player name: " + AchievementsManager.playerName);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            Debug.Log("Application has quit");
            Application.Quit();
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
