using System;

namespace SunnyTown
{
    /// <summary>
    /// A DialogueMapper is repsonsible for mapping Card objects into Dialogue objects
    /// which can be interpreted by the DialogueManager
    /// </summary>
    public class DialogueMapper
    {
        /// <summary>
        /// Converts a PlotCard into a BinaryOptionDialogue, allowing it to be displayed by the 
        /// Dialogue Manager
        /// </summary>
        /// <param name="plotCard">To convert into a BinaryOptionDialogue</param>
        /// <returns>The BinaryOptionDialogue that can be used by the DialogueManager </returns>
        public BinaryOptionDialogue PlotCardToBinaryOptionDialogue(PlotCard plotCard)
        {
            if (plotCard.Options.Count > 1)
            {
                return new BinaryOptionDialogue(plotCard.Question,
                            plotCard.Options[0].Dialogue,
                            plotCard.Options[1].Dialogue,
                            new SimpleDialogue(
                            plotCard.PrecedingDialogue,
                            plotCard.Name));
            }

            return new BinaryOptionDialogue(plotCard.Question,
                plotCard.Options[0].Dialogue,
                "",
                new SimpleDialogue(
                    plotCard.PrecedingDialogue,
                    plotCard.Name));
        }

        /// <summary>
        /// Converts a Minor into a BinaryOptionDialogue, allowing it to be displayed by the 
        /// Dialogue Manager
        /// </summary>
        /// <param name="minorCard">To convert into a BinaryOptionDialogue</param>
        /// <returns>The BinaryOptionDialogue that can be used by the DialogueManager </returns>
        public BinaryOptionDialogue MinorCardToBinaryOptionDialogue(MinorCard minorCard)
        {

            return new BinaryOptionDialogue(minorCard.Question,
                minorCard.Options[0].Dialogue,
                minorCard.Options[1].Dialogue,
                new SimpleDialogue(
                    minorCard.PrecedingDialogue,
                    minorCard.Name));
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
            return new SliderOptionDialogue(
                currentCard.Question,
                currentCard.MaxValue,
                currentCard.MinValue,
                new SimpleDialogue(
                    currentCard.PrecedingDialogue,
                    currentCard.Name));
        }
    }
}