using System.Collections.Generic;

namespace SunnyTown
{
    /// <summary>
    /// A Card represents a interactable decision in the game associated with
    /// dialogue, and a character.
    /// </summary>
    public abstract class Card
    {
        public string[] PrecedingDialogue { get; set; }
        public string Question { get; set; }
        public List<Transition> Options { get; protected set; }
        public string NPCName { get; protected set; }
        public string Feedback { get; protected set; }
        public string FeedbackNPCName { get; protected set; }
        public bool ShouldAnimate { get; protected set; } = false;
        public string BuildingName { get; protected set; } = "";
        public abstract void HandleDecision(int decisionIndex, string additionalState = "");
    }
}