using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SunnyTown
{
    /// <summary>
    /// A CardFactory is responsible for creating specified Card types
    /// </summary>
    public class CardFactory
    {
        private Reader reader;
        public PlotCard CurrentPlotCard { get; private set; }
        private List<Card> minorCards;

        // TODO: Change so this does not have to be hard-coded
        public int TotalPlotCardsInLevel { get; private set; } = 5;
        public int PlotCardsRemaining { get; private set; } = 5;

        public CardFactory()
        {
            reader = new Reader();
            CurrentPlotCard = reader.RootState;
            minorCards = new List<Card>(reader.AllMinorStates);

            minorCards.Randomize();
        }

        /// <summary>
        /// Returns a Card Type based on its descriptor
        /// </summary>
        /// <param name="cardDescriptor">Describes the Card type to be returned</param>
        /// <returns>The specified Card</returns>
        public Card GetNewCard(string cardDescriptor)
        {

            switch (cardDescriptor)
            {
                case ("story"):
                    string nextStateId = string.IsNullOrEmpty(CurrentPlotCard.NextStateId)
                        ? CurrentPlotCard.Id
                        : CurrentPlotCard.NextStateId;
                    CurrentPlotCard = reader.AllStoryStates.Single(s => s.Id.Equals(nextStateId));
                    PlotCardsRemaining--;
                    return CurrentPlotCard;
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
