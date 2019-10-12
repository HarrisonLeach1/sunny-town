using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    public static AchievementsManager Instance { get; private set; }

    public GameObject AchievementsPrefab;
    private Transform highScoreContainer;
    private Transform highScoreTemplate;
    private GameObject achievementsView;

    private const String NUMBER_OF_SCORES = "NumberOfScores";
    private const String HIGH_SCORE = "HighScore";
    private const String PLAYER_NAME = "PlayerName";
    private const int HIGH_SCORE_SIZE = 5;

    public void IsAchievementMade()
    {
        
    }

    public void Awake()
    {
        Debug.Log("awoken");
        achievementsView = Instantiate(AchievementsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        var parentObject = GameObject.Find("AchievementsMenu");
        achievementsView.transform.SetParent(parentObject.transform, false);
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < 7; i++)
        {
            UpdateHighScores(i);
        }
        DisplayHighScores();
    }
    
    public int UpdateHighScores(int newScore)
    {
        var numberOfScores = PlayerPrefs.GetInt(NUMBER_OF_SCORES);
        if (numberOfScores < HIGH_SCORE_SIZE)
        {
            PlayerPrefs.SetInt(HIGH_SCORE + numberOfScores, newScore);
            PlayerPrefs.SetString(PLAYER_NAME + numberOfScores, "BOB");
            PlayerPrefs.SetInt(NUMBER_OF_SCORES, numberOfScores + 1);
            return numberOfScores;
        }
        else
        {
            for (int i = 0; i < HIGH_SCORE_SIZE; i++)
            {
                if (newScore > PlayerPrefs.GetInt(HIGH_SCORE + i))
                {
                    PlayerPrefs.SetInt(HIGH_SCORE + (i + 1), newScore);
                    PlayerPrefs.SetString(PLAYER_NAME + (i + 1), "Bob");
                    return i + 1;
                }
            }
        }

        return -1;
    }

    public List<HighScoreEntry> GetHighScores()
    {
        var highScores = new List<HighScoreEntry>();
        for (var i = 0; i < PlayerPrefs.GetInt(NUMBER_OF_SCORES); i++)
        {
            var hse = new HighScoreEntry(
                PlayerPrefs.GetInt(HIGH_SCORE + i), 
                PlayerPrefs.GetString(PLAYER_NAME + i),
                i + 1);
            highScores.Add(hse);
        }

        return highScores;
    }

    private void DisplayHighScores()
    {
        highScoreContainer = achievementsView.transform.GetChild(0).GetChild(4).GetComponent<Transform>();
        highScoreTemplate = highScoreContainer.Find("HighScoreTemplate");
        highScoreTemplate.gameObject.SetActive(false);

        float templateHeight = 23f;
        var highScoreList = GetHighScores();
        for (int i = 0; i < highScoreList.Count; i++) 
        {
            var entryTransform = Instantiate(highScoreTemplate, highScoreContainer);
            var entryRectTransform = entryTransform.GetComponent<RectTransform>();
            var highScore = entryRectTransform.GetChild(1).GetComponent<TextMeshProUGUI>();
            var playerName = entryRectTransform.GetChild(0).GetComponent<TextMeshProUGUI>();
            var hsrank = entryRectTransform.GetChild(2).GetComponent<TextMeshProUGUI>();
            var hse = highScoreList.ElementAt(i);
            highScore.SetText(hse.getHighScore().ToString());
            playerName.SetText(hse.getPlayerName());
            hsrank.SetText(hse.getRank().ToString());
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);
        }
    }

    public class HighScoreEntry
    {
        internal HighScoreEntry(int highScore, string playername, int rank)
        {
            this.highScore = highScore;
            this.playername = playername;
            this.rank = rank;
        }
        
        private int highScore;
        private string playername;
        private int rank;

        internal int getHighScore()
        {
            return this.highScore;
        }

        internal string getPlayerName()
        {
            return this.playername;
        }

        internal int getRank()
        {
            return this.rank;
        }
    }
}
