using System.Linq;
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

        switch (cardDescriptor)
        {
            case ("story"):
                // TODO: add some error handling here, because right now we are assuming NextState has been set
                // also the users of this class are unaware that state should be changed on the current card
                string nextStateId = currentStoryCard.NextStateId ?? currentStoryCard.Id;
                Debug.Log("Next State: " + nextStateId);
                currentStoryCard = reader.AllStates.Single(s => s.Id.Equals(nextStateId));
                return currentStoryCard;
            case ("minor"):
                return currentMinorCard;
            default:
                throw new System.ArgumentException("Argument invalid for CardFactory");
        }
    }
}