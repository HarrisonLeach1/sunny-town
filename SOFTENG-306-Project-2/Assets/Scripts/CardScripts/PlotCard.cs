using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotCard : Card
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string NextStateId { get; private set; }
    public PlotCard(string id, string[] precedingDialogue, string name, string question, List<Transition> options)
    {
        PrecedingDialogue = precedingDialogue;
        Id = id;
        Name = name;
        Question = question;
        Options = options;
    }

    public override void HandleDecision(int decisionIndex)
    {
        if (Options.Count >= decisionIndex + 1)
        {
            Options[decisionIndex].MetricsModifier.Modify();
            Feedback = Options[decisionIndex].Feedback;
            NextStateId = Options[decisionIndex].NextStateId;
            ShouldAnimate = Options[decisionIndex].HasAnimation;
            BuildingName = Options[decisionIndex].BuildingName;
        }
    }
}