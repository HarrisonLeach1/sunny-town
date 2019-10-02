using UnityEngine;

public class Transition
{
    public string Dialogue { get; set; }
    public string Feedback { get; set; }
    public string NextStateId { get; set; }
    public Transition(string dialogue, string feedback, string nextStateId)
    {
        Dialogue = dialogue;
        Feedback = feedback;
        NextStateId = nextStateId;
    }
}