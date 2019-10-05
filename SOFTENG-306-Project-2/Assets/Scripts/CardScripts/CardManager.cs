using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    [SerializeField]
    private GameObject cardPrefab;
    private GameObject displayedCard;

    private CardFactory cardFactory;
    private DialogueManager dialogueManager;
    private DialogueMapper dialogueMapper;
    private int cardCount = 0;

    private Card currentCard;
    public bool isFinalCard = false;
    private Reader reader;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        reader = new Reader();
        currentCard = reader.RootState;
        cardFactory = new CardFactory();
        dialogueManager = DialogueManager.Instance;
        dialogueMapper = new DialogueMapper();
    }

    public void StartDisplayingCards()
    {
        currentCard = cardFactory.GetNewCard("story");
        StartCoroutine(QueueCard());
    }
    
    private IEnumerator QueueCard()
    {
        yield return new WaitForSeconds(3);
        if (currentCard is PlotCard)
        {
            dialogueManager.StartBinaryOptionDialogue(dialogueMapper.PlotCardToBinaryOptionDialogue((PlotCard) currentCard), HandleOptionPressed);
        }
        else
        {
            dialogueManager.StartBinaryOptionDialogue(dialogueMapper.MinorCardToBinaryOptionDialogue((MinorCard) currentCard), HandleOptionPressed);
        }
    }

    public void HandleOptionPressed(int decisionIndex)
    {
        currentCard.HandleDecision(decisionIndex);
        StartCoroutine(AnimateDecision());
    }

    private IEnumerator AnimateDecision()
    {
        yield return new WaitForSeconds(3);
        ShowFeedback();
    }

    private void ShowFeedback()
    {
        dialogueManager.StartExplanatoryDialogue(dialogueMapper.FeedbackToDialogue(currentCard.Feedback), HandleFeedbackContinued);
    }

    private void HandleFeedbackContinued()
    {
        UpdateCard();
        StartCoroutine(QueueCard());
    }

    private void UpdateCard()
    {
        cardCount++;

        if (IsFinalCard(currentCard))
        {
            isFinalCard = true;
            EndGame();
            return;
        }
        
        if (cardCount % 3 == 0)
        {
            currentCard = cardFactory.GetNewCard("story");
        }
        else
        {
            currentCard = cardFactory.GetNewCard("minor");
        }
    }

    IEnumerator wait(int seconds, Button button1)
    {
        yield return new WaitForSeconds(seconds);
        var text1 = button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        button1.gameObject.SetActive(true);
        button1.onClick.AddListener(() => this.UpdateCard());
        text1.text = "continue";
    }

    private void EndGame()
    {
        // TODO: Handle ending the game, should play a cutscene

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private bool IsFinalCard(Card currentCard)
    {
        // Game is ended on story cards with no transitions
        if (currentCard is PlotCard && currentCard.Options.Count == 0)
        {
            return true;
        }
        return false;
    }

}