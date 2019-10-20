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
        
        private const string LEVEL_TWO_STATE_ID = "s4";
        private const string LEVEL_THREE_STATE_ID = "s10EV";

        public CardFactory()
        {
            reader = Reader.Reset();
            CurrentPlotCard = reader.RootState;
            minorCards = new List<Card>(reader.AllMinorStates);

            //minorCards.Randomize();
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

        public Card GetLevelTwoCard()
        {
            var card = reader.AllStoryStates.Single(s => s.Id.Equals(LEVEL_TWO_STATE_ID));
            CurrentPlotCard = card;
            return card;
        }
        
        public Card GetLevelThreeCard()
        {
            var card = reader.AllStoryStates.Single(s => s.Id.Equals(LEVEL_THREE_STATE_ID));
            CurrentPlotCard = card;
            return card;
        }
    }
}
