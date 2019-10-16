using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressScript : MonoBehaviour
{
    [SerializeField]
    private Slider levelProgressBar;
    [SerializeField]
    private TextMeshProUGUI daysRemainingText;

    internal void UpdateValue(int plotCardsRemaining, int totalPlotCardsInLevel)
    {
        Debug.Log("plot cards remaining: " + plotCardsRemaining);
        float numberOfCards = totalPlotCardsInLevel;
        float remainingPercent = ((numberOfCards - plotCardsRemaining) / numberOfCards) * 100f;
        levelProgressBar.value = remainingPercent;
        Debug.Log("Updated: " + levelProgressBar.value);
        daysRemainingText.text = plotCardsRemaining.ToString();
    }
}
