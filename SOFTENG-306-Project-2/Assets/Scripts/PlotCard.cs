using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotCard : Card
{
    public string Id { get; set; }
    
    public string Label { get; set; }
    public List<Transition> Transitions { get; set; }
    public string NextStateId { get; private set; }

    public PlotCard(string id, string label, string dialogue, List<Transition> transitions)
    {
        Id = id;
        Label = label;
        Dialogue = dialogue;
        Transitions = transitions;
    }

    public PlotCard(string dialogue, List<Transition> transitions)
    {
        Dialogue = dialogue;
        Transitions = transitions;
    }

    public override void HandleDecision(int decisionIndex)
    {
        if (Transitions.Count >= decisionIndex + 1)
        {
            NextStateId = Transitions[decisionIndex].NextStateId;
        } 
        else
        {
            NextStateId = Id;
        }
    }
}