using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    [SerializeField]
    private GameObject cardPrefab;
    private GameObject displayedCard;
    
    private CardFactory cardFactory;
    private DialogueManager dialogueManager;
    private int cardCount = 0;

    private Card currentCard;
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
    }

    public void StartDisplayingCards()
    {
        currentCard = cardFactory.GetNewCard("story");
        string[] statements = new string[2] { "yeet", "hi" };
        dialogueManager.StartBinaryOptionDialogue(new BinaryOptionDialogue(currentCard.Dialogue, currentCard.Options[0].Dialogue, currentCard.Options[1].Dialogue, statements, "jeet"));
        RenderStoryCard();
    }

    public void MakeStoryTransition(int decisionIndex)
    {
        currentCard.HandleDecision(decisionIndex);
        ShowFeedback(decisionIndex);
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

        decisionDialogue.text = currentCard.Feedback;

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
        if (cardCount % 3 == 0)
        {
            currentCard = cardFactory.GetNewCard("story");
            RenderStoryCard();
            MetricManager.Instance.RenderMetrics();
        }
        else
        {
            currentCard = cardFactory.GetNewCard("minor");
            RenderMinorCard();
            MetricManager.Instance.RenderMetrics();
        }
    }

    IEnumerator wait(int seconds, Button button1)
    {
        yield return new WaitForSeconds(seconds);
        var text1 = button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        button1.gameObject.SetActive(true);
        button1.onClick.AddListener(() => this.MakeTransition());
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

        if (currentCard.Options.Count != 0)
        {
            text1.text = currentCard.Options[0].Dialogue;
            text2.text = currentCard.Options[1].Dialogue;
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
        var text2 = button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        button1.onClick.AddListener(() => this.MakeStoryTransition(0));
        button2.onClick.AddListener(() => this.MakeStoryTransition(1));

        decisionDialogue.text = this.currentCard.Dialogue;
        Debug.Log("Found minor card options: " + currentCard.Options.Count);

        if (currentCard.Options.Count != 0)
        {
            text1.text = currentCard.Options[0].Dialogue;
            text2.text = currentCard.Options[1].Dialogue;
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
        if (currentCard is PlotCard && currentCard.Options.Count == 0)
        {
            return true;
        }
        return false;
    }
    
}