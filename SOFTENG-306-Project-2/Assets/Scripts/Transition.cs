using System.Collections.Generic;
using UnityEngine;

public class Transition : Option
{
    public string NextStateId { get; set; }
    public Transition(string dialogue, string feedback, Dictionary<string, int> metrics, string nextStateId) : base(dialogue, feedback, metrics)
    {
        NextStateId = nextStateId;
    }
}