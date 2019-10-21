using System;
using System.Linq;

namespace SunnyTown
{
    /// <summary>
    /// A DialogueMapper is repsonsible for mapping Card objects into Dialogue objects
    /// which can be interpreted by the DialogueManager
    /// </summary>
    public class DialogueMapper
    {
        /// <summary>
        /// Converts a Card into an OptionDialogue, allowing it to be displayed by the 
        /// Dialogue Manager.
        /// </summary>
        /// <param name="card">To convert into a OptionDialogue</param>
        /// <returns>The OptionDialogue that can be used by the DialogueManager</returns>
        public OptionDialogue CardToOptionDialogue(Card card)
        {
            var cardType = card is PlotCard ? "Story" : card is MinorCard ? "Minor" : "";
            string[] optionDialogues = card.Options.Select(o => o.Dialogue).ToArray();
            return new OptionDialogue(card.Question,
                optionDialogues,
                new SimpleDialogue(
                    card.PrecedingDialogue,
                    card.NPCName), cardType
);
        }

        /// <summary>
        /// Converts feedback into a SimpleDialogue allowing it to be displayed by the 
        /// DialogueManager
        /// </summary>
        /// <param name="feedback">feedback to display</param>
        /// <param name="name">name to diplay</param>
        /// <returns>The SimpleDialogue that can be used by the DialogueManager </returns>
        public SimpleDialogue FeedbackToDialogue(string feedback, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                name = "Board of Advisors";
            }
            return new SimpleDialogue(new string[] { feedback }, name);
        }

        /// <summary>
        /// Converts a SliderCard into a SliderOptionDialogue allowing it to be displayed by the 
        /// DialogueManager
        /// </summary>
        /// <param name="currentCard">card to display</param>
        /// <returns>The SliderOptionDialogue that can be used by the DialogueManager </returns>
        public SliderOptionDialogue SliderCardToSliderOptionDialogue(SliderCard currentCard)
        {
            var cardType = "Minor";
            return new SliderOptionDialogue(
                currentCard.Question,
                currentCard.MaxValue,
                currentCard.MinValue,
                new SimpleDialogue(
                    currentCard.PrecedingDialogue,
                    currentCard.NPCName), cardType);
        }
    }
}