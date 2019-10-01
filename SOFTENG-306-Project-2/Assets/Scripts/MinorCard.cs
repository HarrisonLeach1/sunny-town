using System.Collections.Generic;
using UnityEngine;

public class MinorCard : Card {
    public List<Option> Options { get; private set; }

    public MinorCard(string dialogue, List<Option> options)
    {
        Dialogue = dialogue;
        Options = options;
    }

    public override void HandleDecision(int decisionIndex)
    {
        Debug.Log("You made decision: " + decisionIndex);
    }
}
