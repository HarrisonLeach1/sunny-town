public class Transition
{
    public string Dialogue { get; set; }
    public string NextStateId { get; set; }
    public Transition(string dialogue, string nextStateId)
    {
        Dialogue = dialogue;
        NextStateId = nextStateId;
    }
}