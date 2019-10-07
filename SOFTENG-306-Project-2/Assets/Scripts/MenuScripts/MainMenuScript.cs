using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SunnyTown
{
    public class MainMenuScript : MonoBehaviour
    {
        public void PlayGame()
        {
            Debug.Log("Start game");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            Debug.Log("Application has quit");
            Application.Quit();
        }
    }
}
