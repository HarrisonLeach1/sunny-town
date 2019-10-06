using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SunnyTown
{

    public class CardFactory
    {
        private Reader reader;
        private PlotCard currentPlotCard;
        private List<Card> minorCards;

        public CardFactory()
        {
            reader = new Reader();
            currentPlotCard = reader.RootState;
            minorCards = new List<Card>(reader.AllMinorStates);

            // Preserve order now for showcase
            minorCards.Randomize();
        }

        public Card GetNewCard(string cardDescriptor)
        {

            switch (cardDescriptor)
            {
                case ("story"):
                    string nextStateId = string.IsNullOrEmpty(currentPlotCard.NextStateId)
                        ? currentPlotCard.Id
                        : currentPlotCard.NextStateId;
                    currentPlotCard = reader.AllStoryStates.Single(s => s.Id.Equals(nextStateId));
                    return currentPlotCard;
                case ("minor"):
                    if (minorCards.Count == 0)
                    {
                        minorCards = new List<Card>(reader.AllMinorStates);
                        minorCards.Randomize();
                    }

                    var minorCard = minorCards.First();
                    minorCards.Remove(minorCard);
                    return minorCard;
                default:
                    throw new System.ArgumentException("Argument invalid for CardFactory");
            }
        }
    }
}
