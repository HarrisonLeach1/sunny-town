using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SunnyTown;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsManager : MonoBehaviour
{
    public static AchievementsManager Instance { get; private set; }

    public GameObject AchievementsPrefab;
    private Transform highScoreContainer;
    private Transform achievementsContainer;
    private Transform achievementsTemplate;
    private Transform highScoreTemplate;
    private GameObject achievementsView;

    private const String NUMBER_OF_SCORES = "NumberOfScores";
    private const string NUMBER_OF_ACHIEVEMENTS = "NumberOfAchievements";
    private const String HIGH_SCORE = "HighScore";
    private const String PLAYER_NAME = "PlayerName";
    private const int HIGH_SCORE_SIZE = 5;

    private int envInARow;

    private AchievementsManager()
    {
        envInARow = 0;
    }
    
    public void IsAchievementMade()
    {
        if (MetricManager.Instance.EnvHealth >= MetricManager.Instance.PrevEnvHealth)
        {
            envInARow++;
        }
        else
        {
            envInARow = 0;
        }

        if (envInARow == 5)
        {
            if (!IsAchievementAlreadyEarned("Tree Hugger"))
            {
                PlayerPrefs.SetString("achievement" + PlayerPrefs.GetInt(NUMBER_OF_ACHIEVEMENTS), "Tree Hugger");
                PlayerPrefs.SetInt(NUMBER_OF_ACHIEVEMENTS, PlayerPrefs.GetInt(NUMBER_OF_ACHIEVEMENTS) + 1);
                DisplayAchievementNotification("Tree Hugger");
            }
        }
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        achievementsView = Instantiate(AchievementsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        var parentObject = GameObject.Find("AchievementsMenu");
        achievementsView.transform.SetParent(parentObject.transform, false);
        DisplayHighScores();
        DisplayAchievementsMenu();
    }
    

    public static int UpdateHighScores(int newScore)
    {
        var numberOfScores = PlayerPrefs.GetInt(NUMBER_OF_SCORES);
        if (numberOfScores < HIGH_SCORE_SIZE)
        {
            PlayerPrefs.SetInt(HIGH_SCORE + numberOfScores, newScore);
            PlayerPrefs.SetString(PLAYER_NAME + numberOfScores, "BOB");
            PlayerPrefs.SetInt(NUMBER_OF_SCORES, numberOfScores + 1);
            return numberOfScores + 1;
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
        highScoreContainer = achievementsView.transform.GetChild(1).GetChild(4).GetComponent<Transform>();
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

    private bool IsAchievementAlreadyEarned(string achievementName)
    {
        for (int i = 1; i < PlayerPrefs.GetInt(NUMBER_OF_ACHIEVEMENTS); i++)
        {
            if (PlayerPrefs.GetString("achievement" + i).Equals(achievementName))
            {
                return true;
            }
        }

        return false;
    }

    private Achievement GetAchievementByIndex(int i)
    {
        var achievementName = PlayerPrefs.GetString("achievement" + i);
        foreach (Achievement achievement in new Reader().AllAchievements)
        {
            if (achievement.name.Equals(achievementName))
            {
                return achievement;
            }
        }
        return null;
    }

    private void DisplayAchievementsMenu()
    {
        achievementsContainer = achievementsView.transform.GetChild(2).GetChild(2).GetChild(0).GetChild(0).GetComponent<Transform>();
        achievementsTemplate = achievementsContainer.Find("AchievementsTemplate");
        achievementsTemplate.gameObject.SetActive(false);
        float templateHeight = 34f;
        for (int i = 0; i < PlayerPrefs.GetInt(NUMBER_OF_ACHIEVEMENTS); i++)
        {
            var entryTransform = Instantiate(achievementsTemplate, achievementsContainer);
            var entryRectTransform = entryTransform.GetComponent<RectTransform>();
            var badge = entryRectTransform.GetChild(0).GetComponent<Image>();
            var description = entryRectTransform.GetChild(1).GetComponent<TextMeshProUGUI>();
            var date = entryRectTransform.GetChild(2).GetComponent<TextMeshProUGUI>();
            Achievement achievement = GetAchievementByIndex(i); 
            description.SetText("asdfasdf");
            date.SetText("asdfasdfasf");
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);
        }
    }

    public void DisplayAchievementNotification(string achievementName)
    {
        foreach (Achievement a in new Reader().AllAchievements)
        {
            if (a.name.Equals(achievementName))
            {
                Debug.Log("Show achievement badge and notification for: " + achievementName);
            }
        }
    }

    private void DisplayAchievementsEndGame()
    {
        
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
