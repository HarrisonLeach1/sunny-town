using System.Collections.Generic;
using UnityEngine;

public class MinorCard : Card
{
    public MinorCard(string[] precedingDialogue, string dialogue, List<Transition> options)
    {
        PrecedingDialogue = precedingDialogue;
        Question = dialogue;
        Options = options;
    }

    public override void HandleDecision(int decisionIndex)
    {
        if (Options.Count >= decisionIndex + 1)
        {
            Options[decisionIndex].MetricsModifier.Modify();
            Feedback = Options[decisionIndex].Feedback;
            FeedbackNPCName = Options[decisionIndex].FeedbackNPCName;
            ShouldAnimate = Options[decisionIndex].HasAnimation;
            BuildingName = Options[decisionIndex].BuildingName;
        }
    }
}
