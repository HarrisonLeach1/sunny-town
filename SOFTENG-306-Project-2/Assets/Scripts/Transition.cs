public class Transition
{
    public string Dialogue { get; set; }
    public State NextState { get; set; }
    public Transition(string dialogue, State nextState)
    {
        Dialogue = dialogue;
        NextState = nextState;
    }
}