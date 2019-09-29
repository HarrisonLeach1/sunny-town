using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCard : Card
{
    public List<Transition> Transitions { get; set; }
    public StoryCard NextState {get; private set;}
    public StoryCard(string dialogue, List<Transition> transitions)
    {
        Dialogue = dialogue;
        Transitions = transitions;
    }

    public override void HandleDecision(int decisionIndex)
    {
        NextState = Transitions[decisionIndex].NextState;
    }
}