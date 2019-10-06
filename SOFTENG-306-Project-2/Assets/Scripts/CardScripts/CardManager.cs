using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SunnyTown
{

    /// <summary>
    /// A CardManager handles the state of cards and makes calls to the DialogueManager
    /// to render the appropriates Cards
    /// </summary>
    public class CardManager : MonoBehaviour
    {
        public const float time = 5f;
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

        /// <summary>
        /// Queues the dialogue to be played to the user when the current card 
        /// dissappears
        /// </summary>
        /// <param name="endGameDialogue">The dialogue to be played to the user</param>
        public void QueueEndDialogue(SimpleDialogue endGameDialogue)
        {
            gameLost = true;
            this.endGameDialogue = endGameDialogue;
        }

        /// <summary>
        /// Displays a minor card to the user, ensuring not to interrupt any already being
        /// viewed
        /// </summary>
        /// <param name="button">To be destroyed</param>
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
                string[] statements = { "A new message has been addressed to you at the town hall !" };
                dialogueManager.StartExplanatoryDialogue(new SimpleDialogue(statements, "You have mail"), DisplayMinorDecisionCard);
            }
            else
            {
                Debug.Log("Cant select exclamation mark while processing card!");
                // TODO: DisplayWarningDialogue();
            }
        }

        /// <summary>
        /// Displays a minor decision card to the user instantly
        /// </summary>
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

        /// <summary>
        /// Returns whether or not the  user is currently interacting with a card
        /// </summary>
        /// <returns>true if a user is currently viewing a card, otherwise false</returns>
        public bool GetCardStatus()
        {
            return this.currentlyProcessingCard;
        }

        /// <summary>
        /// Begins displaying cards to the user
        /// </summary>
        public void StartDisplayingCards()
        {
            currentCard = cardFactory.GetNewCard("story");
            currentlyProcessingCard = false;
            cardWaitingRoutine = StartCoroutine(QueueCard());
        }

        /// <summary>
        /// Handles when a user makes a decision for a card
        /// </summary>
        /// <param name="decisionValue">The value chosen by the user</param>
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

        /// <summary>
        /// A Coroutine which waits for the animation to be played to the user
        /// </summary>
        /// <param name="time">The time to wait</param>
        /// <returns>The coroutine IEnumerator</returns>
        private IEnumerator WaitForAnimation(float time)
        {
            yield return new WaitForSeconds(time);
            metricManager.RenderMetrics();
            ShowFeedback();
        }

        /// <summary>
        /// A Coroutine which waits for feedback to display to the user
        /// </summary>
        /// <returns>The coroutine IEnumerator</returns>
        private IEnumerator WaitForFeedback()
        {
            yield return new WaitForSeconds(0.5f);
            metricManager.RenderMetrics();
            ShowFeedback();
        }

        /// <summary>
        /// Shows the feedback card to the user based on their decision
        /// </summary>
        private void ShowFeedback()
        {
            dialogueManager.StartExplanatoryDialogue(dialogueMapper.FeedbackToDialogue(currentCard.Feedback, currentCard.FeedbackNPCName), GoToNextCard);
        }

        /// <summary>
        /// A Coroutine which queues the current card to be interacted with
        /// </summary>
        /// <returns>The coroutine IEnumerator</returns>
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

        /// <summary>
        /// Transitions the user to their next card based on their decisions and the game state
        /// </summary>
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

            currentCard = cardFactory.GetNewCard("story");

            currentlyProcessingCard = false;
            cardWaitingRoutine = StartCoroutine(QueueCard());
        }

        /// <summary>
        /// Ends the game, which loads in the end game screen
        /// </summary>
        private void EndGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        /// <summary>
        /// Returns whether or not the Card is the final card
        /// </summary>
        /// <param name="currentCard">The card to assert finality of</param>
        /// <returns>True if the current card is a final card of the story tree, otherwise false</returns>
        private bool IsFinalCard(Card currentCard)
        {
            // Game is ended on story cards with no transitions

            if (currentCard is PlotCard)
            {
                if (((PlotCard)currentCard).NextStateId == null)
                {
                    return true;
                }
            }
            return false;
        }

    }
}