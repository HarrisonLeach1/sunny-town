public class DialogueMapper
{
    public BinaryOptionDialogue ToBinaryOptionDialogue(Card card)
    {
        string[] statements = new string[2] { "Hello Mayor lovely day isn't it?", "My farm hasn't been doing so well and I was wondering if you could help me out" };

        return new BinaryOptionDialogue(card.Dialogue, 
            card.Options[0].Dialogue, 
            card.Options[1].Dialogue, 
            new SimpleDialogue(
            statements, 
            "Old Farmer John"));
    }
}