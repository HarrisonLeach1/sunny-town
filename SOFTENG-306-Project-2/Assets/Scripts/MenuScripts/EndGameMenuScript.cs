using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameMenuScript : MonoBehaviour
{
     TextMeshPro gameOutcome;

    void Start()
    {
        gameOutcome = GameObject.Find("GameOutcome").GetComponent<TextMeshPro>();
        Debug.Log(gameOutcome);
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
