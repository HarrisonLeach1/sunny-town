using System.Collections;
using System.Collections.Generic;

public class StoryCard
{
    public string Id { get; set; }
    public string Dialogue { get; set; }
    public List<Transition> Transitions { get; set; }

    public StoryCard(string id, string dialogue, List<Transition> transitions)
    {
        Id = id;
        Dialogue = dialogue;
        Transitions = transitions;
    }
    
    public StoryCard(string dialogue, List<Transition> transitions)
    {
        Dialogue = dialogue;
        Transitions = transitions;
    }
}