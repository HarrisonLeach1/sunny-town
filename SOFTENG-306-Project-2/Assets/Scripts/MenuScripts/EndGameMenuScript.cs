using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameMenuScript : MonoBehaviour
{
     GameObject screen;

    void Start()
    {
        var text = screen.transform.GetChild(0).GetComponent<TextMeshPro>();
        text.SetText("asdfasdf");

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
