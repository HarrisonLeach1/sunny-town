public abstract class Card
{
    public string Dialogue { get; set; }
    public abstract void HandleDecision(int decisionIndex);
}