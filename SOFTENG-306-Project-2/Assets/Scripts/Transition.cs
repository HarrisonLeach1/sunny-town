public class Transition
{
    public string Dialogue { get; set; }
    public StoryCard NextState { get; set; }
    public Transition(string dialogue, StoryCard nextState)
    {
        Dialogue = dialogue;
        NextState = nextState;
    }
}