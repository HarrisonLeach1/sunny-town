using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SunnyTown;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsManager : MonoBehaviour
{
    public static AchievementsManager Instance { get; private set; }

    public GameObject AchievementsPrefab;
    public GameObject AchievementNotification;
    private Transform highScoreContainer;
    private Transform achievementsContainer;
    private Transform achievementsTemplate;
    private Transform highScoreTemplate;
    private GameObject achievementsView;

    private const string NUMBER_OF_SCORES = "NumberOfScores";
    private const string NUMBER_OF_ACHIEVEMENTS = "NumberOfAchievements";
    private const string HIGH_SCORE = "HighScore";
    private const string PLAYER_NAME = "PlayerName";
    private const int HIGH_SCORE_SIZE = 5;

    private int envInARow;

    private AchievementsManager()
    {
        envInARow = 0;
    }
    
    public void IsAchievementMade()
    {
        HandleWinnerAchievement();
        HandleTreeHuggerAchievement();
    }

    private void HandleTreeHuggerAchievement()
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

    private void HandleWinnerAchievement()
    {
        if (CardManager.Instance.GameWon && !IsAchievementAlreadyEarned("Winner"))
        {
            PlayerPrefs.SetString("achievement" + PlayerPrefs.GetInt(NUMBER_OF_ACHIEVEMENTS), "Winner");
            PlayerPrefs.SetInt(NUMBER_OF_ACHIEVEMENTS, PlayerPrefs.GetInt(NUMBER_OF_ACHIEVEMENTS) + 1);
            DisplayAchievementNotification("Winner");
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
    

    /// <summary>
    /// Checks if the new score is within the high score list, if it is, then add it to the list.
    /// </summary>
    /// <param name="newScore">Score to be checked to add to the high score list</param>
    /// <returns>Rank of the new high score, -1 if the score is too low to be a high score</returns>
    public static int UpdateHighScores(int newScore)
    {
        var numberOfScores = PlayerPrefs.GetInt(NUMBER_OF_SCORES);
        for (int i = 1; i <= numberOfScores; i++)
        {
            if (newScore > PlayerPrefs.GetInt(HIGH_SCORE + i))
            {
                LoadHighScoreIntoIndex(newScore, i);
                return i;
            }
        }

        if (numberOfScores < HIGH_SCORE_SIZE)
        {
            LoadHighScoreIntoIndex(newScore, numberOfScores + 1);
            return numberOfScores + 1;
        }

        return -1;
    }

    private static void LoadHighScoreIntoIndex(int newScore, int index)
    {
        //move the positions of all scores below the current one down
        for (int i = PlayerPrefs.GetInt(NUMBER_OF_SCORES); i >= index; i--)
        {
            PlayerPrefs.SetInt(HIGH_SCORE + (i + 1), PlayerPrefs.GetInt(HIGH_SCORE + i));
            PlayerPrefs.SetString(PLAYER_NAME + (i + 1), PlayerPrefs.GetString(PLAYER_NAME + i));
        }
        
        //add in the new score in the indexed position
        PlayerPrefs.SetInt(HIGH_SCORE + index, newScore);
        PlayerPrefs.SetString(PLAYER_NAME + index, "bob");
        PlayerPrefs.SetInt(NUMBER_OF_SCORES, PlayerPrefs.GetInt(NUMBER_OF_SCORES) + 1);

        //if the number of high scores stored are now greater than the maximum, delete the lowest of the high scores
        if (PlayerPrefs.GetInt(NUMBER_OF_SCORES) > HIGH_SCORE_SIZE)
        {
            PlayerPrefs.DeleteKey(HIGH_SCORE + (HIGH_SCORE_SIZE + 1));
            PlayerPrefs.DeleteKey(PLAYER_NAME + (HIGH_SCORE_SIZE + 1));
            PlayerPrefs.SetInt(NUMBER_OF_SCORES, HIGH_SCORE_SIZE);
        }
    }

    public List<HighScoreEntry> GetHighScores()
    {
        var highScores = new List<HighScoreEntry>();
        for (var i = 1; i <= PlayerPrefs.GetInt(NUMBER_OF_SCORES); i++)
        {
            var hse = new HighScoreEntry(
                PlayerPrefs.GetInt(HIGH_SCORE + i), 
                PlayerPrefs.GetString(PLAYER_NAME + i),
                i);
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
        for (int i = 0; i < PlayerPrefs.GetInt(NUMBER_OF_ACHIEVEMENTS); i++)
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
        Debug.Log("saved achievement name: " + achievementName);
        foreach (Achievement achievement in Reader.Instance.AllAchievements)
        {
            Debug.Log("all achievement names: " + achievement.name);
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
        var achievementsCompleted = achievementsView.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        achievementsCompleted.SetText("Achievements Unlocked: " + PlayerPrefs.GetInt(NUMBER_OF_ACHIEVEMENTS) + "/" +
                                      Reader.Instance.AllAchievements.Count);
        
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
            description.SetText(achievement.name + " - " + achievement.description);
            date.SetText("asdfasdfasf");
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);
        }
    }

    public void DisplayAchievementNotification(string achievementName)
    {
        Debug.Log("Show achievement badge and notification for: " + achievementName);
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
