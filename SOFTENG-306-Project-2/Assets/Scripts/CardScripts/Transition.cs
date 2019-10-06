using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    private string nextStateId;
    public string Dialogue { get; set; }
    public string Feedback { get; set; }
    public string NextStateId { get => nextStateId; set => nextStateId = value; }
    public bool HasAnimation { get; protected set; } = false;
    public string BuildingName { get; set; } = "";
    public MetricsModifier MetricsModifier { get; set; }
    public Transition(string feedback, MetricsModifier metricsmodifier, bool hasAnimation, string buildingName, string dialogue = "", string nextStateId = "")
    {
        Debug.Log(" created transition: " + feedback + hasAnimation);
        this.Dialogue = dialogue;
        this.Feedback = feedback;
        this.HasAnimation = hasAnimation;
        this.BuildingName = buildingName;
        this.MetricsModifier = metricsmodifier;
        this.NextStateId = nextStateId;
    }
}