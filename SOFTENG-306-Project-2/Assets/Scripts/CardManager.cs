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

    private StoryCard currentStoryCardData;
    private List<StoryCard> allStates;
    private Reader reader;


    // Start is called before the first frame update
    void Start()
    {
        reader = new Reader();
        allStates = reader.allStates;
        currentStoryCardData = reader.RootState;
        ChangeCard();
    }

    public void MakeTransition(int decisionIndex)
    {
        Debug.Log(currentStoryCardData.Dialogue);
        if (currentStoryCardData.Transitions.Count != 0)
        {
            string nextStateId = currentStoryCardData.Transitions[decisionIndex].NextStateId;
            currentStoryCardData = allStates.Single(s => s.Id.Equals(nextStateId));
            //currentStoryCardData = currentStoryCardData.Transitions[decisionIndex].NextState;
        }
        ChangeCard();
    }

    public void ChangeCard()
    {
        Destroy(displayedCard);
        displayedCard = Instantiate(cardPrefab, new Vector3(0,0, 0), Quaternion.identity);

        var decisionDialogue = displayedCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var button1 = displayedCard.transform.GetChild(1).GetComponent<Button>();
        var button2 = displayedCard.transform.GetChild(2).GetComponent<Button>();
        var text1 = button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        var text2 = button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        button1.onClick.AddListener(() => this.MakeTransition(0));
        button2.onClick.AddListener(() => this.MakeTransition(1));

        decisionDialogue.text = currentStoryCardData.Dialogue;
        if (currentStoryCardData.Transitions.Count != 0)
        {
            text1.text = currentStoryCardData.Transitions[0].Dialogue;
            text2.text = currentStoryCardData.Transitions[1].Dialogue;
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