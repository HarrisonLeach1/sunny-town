using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameMenuScript : MonoBehaviour
{

    public GameObject winEndScreen;
    public GameObject lossEndScreen;
    
    void Awake()
    {
        winEndScreen = GameObject.FindGameObjectWithTag("WinEndScreen");
        lossEndScreen = GameObject.FindGameObjectWithTag("LossEndScreen");
        var gameEnded = CardManager.Instance.isFinalCard;
        Debug.Log("Game ended:" + gameEnded);
        winEndScreen.SetActive(gameEnded);
        lossEndScreen.SetActive(!gameEnded);
        
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
