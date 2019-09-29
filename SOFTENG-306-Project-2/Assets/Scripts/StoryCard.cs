using System.Collections;
using System.Collections.Generic;

public class StoryCard
{
    public string Dialogue { get; set; }
    public List<Transition> Transitions { get; set; }
    public StoryCard(string dialogue, List<Transition> transitions)
    {
        Dialogue = dialogue;
        Transitions = transitions;
    }
}