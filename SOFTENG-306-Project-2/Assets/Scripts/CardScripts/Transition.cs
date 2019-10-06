using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    private string nextStateId;
    public string Dialogue { get; set; }
    public string Feedback { get; set; }
    public string NextStateId { get => nextStateId; set => nextStateId = value; }
    public bool HasAnimation { get; protected set; } = false;
    public MetricsModifier MetricsModifier { get; set; }
    public Transition(string feedback, MetricsModifier metricsmodifier, bool hasAnimation, string dialogue = "", string nextStateId = "")
    {
        this.Dialogue = dialogue;
        this.Feedback = feedback;
        this.HasAnimation = hasAnimation;
        this.MetricsModifier = metricsmodifier;
        this.NextStateId = nextStateId;
    }
}