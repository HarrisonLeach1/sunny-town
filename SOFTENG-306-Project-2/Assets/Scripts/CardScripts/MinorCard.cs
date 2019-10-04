using System.Collections.Generic;
using UnityEngine;

public class MinorCard : Card
{
    public MinorCard(string dialogue, List<Transition> options)
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
        }
    }
}
