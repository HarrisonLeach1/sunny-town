using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Option
{
    public string Dialogue { get; set; }
    public string Feedback { get; set; }
    public MetricsModifier MetricsModifier { get; set; }
    public Option(string dialogue, string feedback, MetricsModifier metricsModifier)
    {
        Dialogue = dialogue;
        Feedback = feedback;
        MetricsModifier = metricsModifier;
    }
}