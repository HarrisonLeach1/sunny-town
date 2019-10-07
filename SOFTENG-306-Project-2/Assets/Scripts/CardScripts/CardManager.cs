using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public const float time = 9f;
    public static CardManager Instance { get; private set; }
    public GameObject spawnHandlerObject;

    private CardFactory cardFactory;
    private DialogueManager dialogueManager;
    private MetricManager metricManager;
    private DialogueMapper dialogueMapper;
    private SpawnHandler animationHandler;
    private int cardCount = 0;

    private Card currentCard;
    public bool isFinalCard = false;
    private Reader reader;
    private bool currentlyProcessingCard = true;
    private Coroutine cardWaitingRoutine;
    private bool gameLost = false;
    private SimpleDialogue endGameDialogue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        metricManager = MetricManager.Instance;
        dialogueMapper = new DialogueMapper();
        animationHandler = spawnHandlerObject.GetComponent<SpawnHandler>();
    }

    public void QueueEndDialogue(SimpleDialogue endGameDialogue)
    {
        gameLost = true;
        this.endGameDialogue = endGameDialogue;
    }

    public void DisplayMinorCard(Button button)
    {
        if (!currentlyProcessingCard)
        {
            Debug.Log("Successfully selected minor card from world!");
            StopCoroutine(cardWaitingRoutine);
            currentlyProcessingCard = true;
            PopupButtonControllerScript.popupShowing = false;
            Destroy(button.gameObject);
            //show exposition dialouge 
            string[] statements = {"A new message has been addressed to you at the town hall !"};
            dialogueManager.StartExplanatoryDialogue( new SimpleDialogue(statements, "You have mail"),DisplayMinorDecisionCard);
        }
        else
        {
            Debug.Log("Cant select exclamation mark while processing card!");
            // TODO: DisplayWarningDialogue();
        }
    }

    public void DisplayMinorDecisionCard()
    {
            currentCard = cardFactory.GetNewCard("minor");
            //destroy the exclamation mark
            if (currentCard is MinorCard)
            {
                dialogueManager.StartBinaryOptionDialogue(dialogueMapper.MinorCardToBinaryOptionDialogue((MinorCard)currentCard), HandleOptionPressed);
            }
            else
            {
                dialogueManager.StartSliderOptionDialogue(dialogueMapper.SliderCardToSliderOptionDialogue((SliderCard)currentCard), HandleOptionPressed);
            }
    }

    public bool GetCardStatus()
    {
        return this.currentlyProcessingCard;
    }
    public void StartDisplayingCards()
    {
        currentCard = cardFactory.GetNewCard("story");
        currentlyProcessingCard = false;
        cardWaitingRoutine = StartCoroutine(QueueCard());
    }

    public void HandleOptionPressed(int decisionValue)
    {
        currentCard.HandleDecision(decisionValue);
        Debug.Log("should animate in handle option pressed: " + currentCard.ShouldAnimate);
        if (string.IsNullOrEmpty(currentCard.Feedback))
        {
            metricManager.RenderMetrics();
            GoToNextCard();
        }
        else if (currentCard.ShouldAnimate)
        {
            var time = 3f;
            Debug.Log(time);
            dialogueManager.ShowAnimationProgress(time);
            StartCoroutine(WaitForAnimation(time));
            animationHandler.PlayAnimation(currentCard.BuildingName, time);
        }
        else
        {
            StartCoroutine(WaitForFeedback());
        }
    }

    private IEnumerator WaitForAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        metricManager.RenderMetrics();
        ShowFeedback();
    }

    private IEnumerator WaitForFeedback()
    {
        yield return new WaitForSeconds(0.5f);
        metricManager.RenderMetrics();
        ShowFeedback();
    }

    private void ShowFeedback()
    {
        dialogueManager.StartExplanatoryDialogue(dialogueMapper.FeedbackToDialogue(currentCard.Feedback, currentCard.FeedbackNPCName), GoToNextCard);
    }

    private IEnumerator QueueCard()
    {
        yield return new WaitForSeconds(CardManager.time);

        currentlyProcessingCard = true;
        if (currentCard is PlotCard)
        {
            dialogueManager.StartBinaryOptionDialogue(dialogueMapper.PlotCardToBinaryOptionDialogue((PlotCard)currentCard), HandleOptionPressed);
        }
        else if (currentCard is MinorCard)
        {
            Debug.Log("Queueing card: " + currentCard.PrecedingDialogue + currentCard.Question);
            dialogueManager.StartBinaryOptionDialogue(dialogueMapper.MinorCardToBinaryOptionDialogue((MinorCard)currentCard), HandleOptionPressed);
        }
        else
        {
            dialogueManager.StartSliderOptionDialogue(dialogueMapper.SliderCardToSliderOptionDialogue((SliderCard)currentCard), HandleOptionPressed);
        }
    }

    private void GoToNextCard()
    {
        cardCount++;

        if (IsFinalCard(currentCard))
        {
            isFinalCard = true;
            EndGame();
            return;
        }
        else if (gameLost)
        {
            dialogueManager.StartExplanatoryDialogue(
                this.endGameDialogue,
                () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
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

        currentlyProcessingCard = false;
        cardWaitingRoutine = StartCoroutine(QueueCard());
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