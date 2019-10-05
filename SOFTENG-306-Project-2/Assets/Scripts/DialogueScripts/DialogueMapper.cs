public class DialogueMapper
{
    public BinaryOptionDialogue PlotCardToBinaryOptionDialogue(PlotCard plotCard)
    {
        //string[] statements = new string[2] { "Hello Mayor lovely day isn't it?", "My farm hasn't been doing so well and I was wondering if you could help me out" };
        string[] statements = new string[0];
            
        return new BinaryOptionDialogue(plotCard.Dialogue, 
            plotCard.Options[0].Dialogue, 
            plotCard.Options[1].Dialogue, 
            new SimpleDialogue(
            statements, 
            plotCard.Name));
    }
    
    public BinaryOptionDialogue MinorCardToBinaryOptionDialogue(MinorCard minorCard)
    {
        //string[] statements = new string[2] { "Hello Mayor lovely day isn't it?", "My farm hasn't been doing so well and I was wondering if you could help me out" };
        string[] statements = new string[0];
            
        return new BinaryOptionDialogue(minorCard.Dialogue, 
            minorCard.Options[0].Dialogue, 
            minorCard.Options[1].Dialogue, 
            new SimpleDialogue(
                statements, 
                ""));
    }
}