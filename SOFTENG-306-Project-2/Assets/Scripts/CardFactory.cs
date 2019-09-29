using UnityEngine;

public class CardFactory
{
    private Reader reader;
    private StoryCard currentStoryCard;
    private Card currentMinorCard;

    public CardFactory()
    {
        reader = new Reader();
        currentStoryCard = reader.RootState;
    }

    public Card GetNewCard(string cardDescriptor)
    {

        switch(cardDescriptor)
        {
            case ("story"):
                currentStoryCard = currentStoryCard.NextState ?? currentStoryCard;
                return currentStoryCard;
            case ("minor"):
                return currentMinorCard;
            default:
                throw new System.ArgumentException("Argument invalid for CardFactory");
        }
    }
}