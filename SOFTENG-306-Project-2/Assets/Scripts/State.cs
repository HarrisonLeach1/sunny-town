using System.Collections;
using System.Collections.Generic;

public class State
{
    public string Dialogue { get; set; }
    public List<Transition> Transitions { get; set; }
    public State(string dialogue, List<Transition> transitions)
    {
        Dialogue = dialogue;
        Transitions = transitions;
    }
}