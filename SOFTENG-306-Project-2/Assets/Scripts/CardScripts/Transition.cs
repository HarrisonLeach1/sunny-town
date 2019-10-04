using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    private string nextStateId;

    public string Dialogue { get; set; }
    public string Feedback { get; set; }
    public string NextStateId { get => nextStateId; set => nextStateId = value; }
    public MetricsModifier MetricsModifier { get; set; }
    public Transition(string dialogue, string feedback, MetricsModifier metricsmodifier, string nextStateId)
    {
        this.Dialogue = dialogue;
        this.Feedback = feedback;
        this.MetricsModifier = metricsmodifier;
        this.NextStateId = nextStateId;
    }
}