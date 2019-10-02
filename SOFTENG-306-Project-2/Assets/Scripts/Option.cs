using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Option
{
    public string Dialogue { get; set; }
    public string Feedback { get; set; }
    public Dictionary<string, int> Metrics { get; set; }
    public Option(string dialogue, string feedback, Dictionary<string, int> metrics)

    {
        Dialogue = dialogue;
        Feedback = feedback;
        Metrics = metrics;
    }
}