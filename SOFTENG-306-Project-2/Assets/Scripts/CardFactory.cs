using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardFactory
{
    private Reader reader;
    private PlotCard _currentPlotCard;
    private List<MinorCard> minorCards;

    public CardFactory()
    {
        reader = new Reader();
        _currentPlotCard = reader.RootState;
        minorCards = new List<MinorCard>(reader.AllMinorStates);
    }

    public Card GetNewCard(string cardDescriptor)
    {

        switch (cardDescriptor)
        {
            case ("story"):
                // TODO: add some error handling here, because right now we are assuming NextState has been set
                // also the users of this class are unaware that state should be changed on the current card
                string nextStateId = _currentPlotCard.NextStateId ?? _currentPlotCard.Id;
                Debug.Log("Next State: " + nextStateId);
                _currentPlotCard = reader.AllStoryStates.Single(s => s.Id.Equals(nextStateId));
                return _currentPlotCard;
            case ("minor"):
                if (minorCards.Count == 0)
                {
                    minorCards = new List<MinorCard>(reader.AllMinorStates);
                }

                // TODO: Add randomness to card selection
                var minorCard = minorCards[0];
                minorCards.Remove(minorCard);
                return minorCard;
            default:
                throw new System.ArgumentException("Argument invalid for CardFactory");
        }
    }
}