using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DecisionController : MonoBehaviour
{
    private StoryCard currentState;
    private TextMeshProUGUI decisionDialogue;
    private TextMeshProUGUI text1;
    private TextMeshProUGUI text2;
    private Reader reader;
    private List<StoryCard> allStates;


    private void Awake()
    {
        decisionDialogue = GameObject.Find("DecisionDialogue").GetComponent<TextMeshProUGUI>();
        text1 = GameObject.Find("Text1").GetComponent<TextMeshProUGUI>();
        text2 = GameObject.Find("Text2").GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        reader = new Reader();
        allStates = reader.allStates;
        currentState = reader.RootState;
        PopulateDecisionDialogue();
    }

    public void MakeTransition(int decisionIndex)
    {
        Debug.Log(currentState.Dialogue);
        if (currentState.Transitions.Count != 0)
        {
            string nextStateId = currentState.Transitions[decisionIndex].NextStateId;
            currentState = allStates.Single(s => s.Id.Equals(nextStateId));
        }
        PopulateDecisionDialogue();
    }

    private void PopulateDecisionDialogue()
    {
        decisionDialogue.text = currentState.Dialogue;

        if (currentState.Transitions.Count != 0)
        {
            text1.text = currentState.Transitions[0].Dialogue;
            text2.text = currentState.Transitions[1].Dialogue;
        } else
        {
            text1.text = "Game Over";
            text2.text = "";
        }
    }
}