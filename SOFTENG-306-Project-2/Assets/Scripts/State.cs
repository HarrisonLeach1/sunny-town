using System.Collections;
using System.Collections.Generic;

public class State
{
    public string Id { get; set; }
    public string Dialogue { get; set; }
    public List<Transition> Transitions { get; set; }

    public State(string id, string dialogue, List<Transition> transitions)
    {
        Id = id;
        Dialogue = dialogue;
        Transitions = transitions;
    }
    public State(string dialogue, List<Transition> transitions)
    {
        Dialogue = dialogue;
        Transitions = transitions;
    }
}