using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField] 
    private GameObject metricPrefab;    
    
    private GameObject displayedCard;
    private GameObject metricPanel;
    
    private CardFactory cardFactory;
    private int nextStoryDecisionIndex;
    private int money = 0;
    private int happiness = 0;
    private int environment = 0;
    private int cardCount = 0;

    private Card currentCard;
    private Reader reader;


    // Start is called before the first frame update
    void Start()
    {
        reader = new Reader();
        currentCard = reader.RootState;
        cardFactory = new CardFactory();
        currentCard = cardFactory.GetNewCard("story");
        RenderStoryCard();
        RenderMetrics();
    }

    public void MakeStoryTransition(int decisionIndex)
    {
        currentCard.HandleDecision(decisionIndex);
        ShowFeedback(decisionIndex);
        UpdateMetrics(decisionIndex);
    }
    
    private void UpdateMetrics(int decisionIndex)
    {
        var card = currentCard as PlotCard;
        Dictionary<string, int> metricsEffects = card.Transitions[decisionIndex].Metrics;
        this.money += metricsEffects["money"];
        this.environment += metricsEffects["environment"];
        this.happiness += metricsEffects["happiness"];
    }

    private void RenderMetrics()
    {
        Destroy(metricPanel);
        metricPanel = Instantiate(metricPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        var money = metricPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var happiness = metricPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        var environment = metricPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        
        money.text = this.money.ToString();
        happiness.text = this.happiness.ToString();
        environment.text = this.environment.ToString();
        
        var parentObject = GameObject.Find("MetricPanel");
        metricPanel.transform.SetParent(parentObject.transform, false);
    }

    private void ShowFeedback(int decisionIndex)
    {
        Destroy(displayedCard);
        displayedCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        var decisionDialogue = displayedCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var button1 = displayedCard.transform.GetChild(1).GetComponent<Button>();
        var button2 = displayedCard.transform.GetChild(2).GetComponent<Button>();
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        var card = currentCard as PlotCard;
        decisionDialogue.text = card.Transitions[decisionIndex].Feedback;

        StartCoroutine(wait(2, button1));

        var parentObject = GameObject.Find("CardPanel");
        displayedCard.transform.SetParent(parentObject.transform, false);
    }

    private void MakeTransition()
    {
        cardCount++;

        if (IsFinalCard(currentCard))
        {
            EndGame();
            return;
        }

        // TODO: Add randomness here
//        if (cardCount % 3 == 0)
        if (true)
        {
            currentCard = cardFactory.GetNewCard("story");
            RenderStoryCard();
            RenderMetrics();
        }
        else
        {
            currentCard = cardFactory.GetNewCard("minor");
            RenderMinorCard();
        }
    }

    IEnumerator wait(int seconds, Button button1)
    {
        yield return new WaitForSeconds(seconds);
        var text1 = button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        button1.gameObject.SetActive(true);
        button1.onClick.AddListener(() => this.MakeTransition());
//        button1.onClick.AddListener(() => this.UpdateMetrics());
//        RenderMetrics();
        var card = currentCard as PlotCard;
        text1.text = "continue";
    }

    // BIG TODO: refactor classes to remove this duplicate logic. Just keeping like this for now, to keep consistent for merging.
    public void RenderStoryCard()
    {
        Destroy(displayedCard);
        displayedCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        var decisionDialogue = displayedCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var button1 = displayedCard.transform.GetChild(1).GetComponent<Button>();
        var button2 = displayedCard.transform.GetChild(2).GetComponent<Button>();
        var text1 = button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var text2 = button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        button1.onClick.AddListener(() => this.MakeStoryTransition(0));
        button2.onClick.AddListener(() => this.MakeStoryTransition(1));

        decisionDialogue.text = currentCard.Dialogue;

        var card = currentCard as PlotCard;
        if (card.Transitions.Count != 0)
        {
            text1.text = card.Transitions[0].Dialogue;
            text2.text = card.Transitions[1].Dialogue;
        }
        else
        {
            text1.text = "Game Over";
            text2.text = "";
        }

        var parentObject = GameObject.Find("CardPanel");

        displayedCard.transform.SetParent(parentObject.transform, false);
    }

    public void RenderMinorCard()
    {
        Destroy(displayedCard);
        displayedCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        var decisionDialogue = displayedCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var button1 = displayedCard.transform.GetChild(1).GetComponent<Button>();
        var button2 = displayedCard.transform.GetChild(2).GetComponent<Button>();
        var text1 = button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        button2.gameObject.SetActive(false);
        var text2 = button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        button1.onClick.AddListener(() => this.MakeTransition());
        button2.onClick.AddListener(() => this.MakeTransition());

        decisionDialogue.text = currentCard.Dialogue;

        var card = currentCard as MinorCard;
        if (card.Options.Count != 0)
        {
            text1.text = card.Options[0].Dialogue;
            text2.text = card.Options[1].Dialogue;
        }
        else
        {
            text1.text = "Game Over";
            text2.text = "";
        }

        var parentObject = GameObject.Find("CardPanel");

        displayedCard.transform.SetParent(parentObject.transform, false);
    }

    private void EndGame()
    {
        // TODO: Handle ending the game, should play a cutscene
    }

    private bool IsFinalCard(Card currentCard)
    {
        // Game is ended on story cards with no transitions
        if (currentCard is PlotCard && (currentCard as PlotCard).Transitions.Count == 0)
        {
            return true;
        }
        return false;
    }
    
}