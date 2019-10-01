using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;
    private GameObject displayedCard;
    private CardFactory cardFactory;

    private Card currentCard;
    private Reader reader;


    // Start is called before the first frame update
    void Start()
    {
        reader = new Reader();
        currentCard = reader.RootState;
        cardFactory = new CardFactory();
        currentCard = cardFactory.GetNewCard("story");
        ChangeCard();
    }

    public void MakeTransition(int decisionIndex)
    {
        currentCard.HandleDecision(decisionIndex);
        currentCard = cardFactory.GetNewCard("story");
        ChangeCard();
    }

    public void ChangeCard()
    {
        Destroy(displayedCard);
        displayedCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        var decisionDialogue = displayedCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var button1 = displayedCard.transform.GetChild(1).GetComponent<Button>();
        var button2 = displayedCard.transform.GetChild(2).GetComponent<Button>();
        var text1 = button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var text2 = button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        button1.onClick.AddListener(() => this.MakeTransition(0));
        button2.onClick.AddListener(() => this.MakeTransition(1));

        // TODO: Add transition dialogues to both Card types
        StoryCard card = currentCard as StoryCard;

        decisionDialogue.text = currentCard.Dialogue;
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
}