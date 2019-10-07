using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SunnyTown
{
    public class EndGameMenuScript : MonoBehaviour
    {

        public GameObject endGameView;
        public GameObject endGamePrefab;

        void Awake()
        {
            endGameView = Instantiate(endGamePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            var gameScore = endGameView.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();
            var gameOutcome = endGameView.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
            var gameBackground = endGameView.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            gameOutcome.SetText(CardManager.Instance.isFinalCard ? "Game Won" : "Game lost");
            if (CardManager.Instance.isFinalCard)
            {
                gameOutcome.SetText("Game Won");
                gameBackground.color = new Color32(0, 255, 0, 130);
            }
            else
            {
                gameOutcome.SetText("Game Lost");
                gameBackground.color = new Color32(255, 0, 0, 130);
            }

            gameScore.SetText("Final Score: " + MetricManager.Instance.GetScore());
        }

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
}
