using System.Collections.Generic;
using UnityEngine;

public class Transition
{
    public string Dialogue { get; set; }
    public string Feedback { get; set; }
    public string NextStateId { get; set; }
    public Dictionary<string, int> Metrics { get; set; }
    public Transition(string dialogue, string feedback, Dictionary<string, int> metrics, string nextStateId)
    {
        Dialogue = dialogue;
        Feedback = feedback;
        NextStateId = nextStateId;
        Metrics = metrics;
    }
}