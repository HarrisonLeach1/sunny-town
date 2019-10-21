using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SunnyTown
{
    /// <summary>
    /// The MainMenuScript is a simple script used to load the Main Menu of the game and all of its components
    /// </summary>
    public class MainMenuScript : MonoBehaviour
    {
        public GameObject PlayerInput;
        public void LoadPlayerName()
        {
            Time.timeScale = 1f;
            Debug.Log("Start game");
            string playerName = PlayerInput.GetComponent<TMP_InputField>().text;
            if (playerName == "")
            {
                AchievementsManager.playerName = "Player";
            }
            else
            {
                AchievementsManager.playerName = playerName;
            }
            Debug.Log("Player name: " + AchievementsManager.playerName);
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            Debug.Log("Application has quit");
            Application.Quit();
        }

        /// <summary>
        /// A method that is used to return back to the menu scene. Not to be used in the main menu itself however, as
        /// it results in the main menu's music restarting.
        /// </summary>
        public void ReturnToMenu()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
