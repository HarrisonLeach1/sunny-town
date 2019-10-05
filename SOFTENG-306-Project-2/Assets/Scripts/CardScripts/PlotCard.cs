using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotCard : Card
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string NextStateId { get; private set; }
    public PlotCard(string id, string name, string dialogue, List<Transition> options)
    {
        Id = id;
        Name = name;
        Dialogue = dialogue;
        Options = options;
    }

    public PlotCard(string dialogue, List<Transition> options)
    {
        Dialogue = dialogue;
        Options = options;
    }

    public override void HandleDecision(int decisionIndex)
    {
        if (Options.Count >= decisionIndex + 1)
        {
            Options[decisionIndex].MetricsModifier.Modify();
            Feedback = Options[decisionIndex].Feedback;
            NextStateId = Options[decisionIndex].NextStateId;
        }
    }
}