using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    /// <summary>
    /// A PlotCard represents an interactable decision which impacts the story tree and 
    /// thus future PlotCards presented to the user.
    /// </summary>
    public class PlotCard : Card
    {
        public string Id { get; set; }
        public string NextStateId { get; private set; }
        

        public PlotCard(string id, string[] precedingDialogue, string name, string question, List<Transition> options)
        {
            PrecedingDialogue = precedingDialogue;
            Id = id;
            NPCName = name;
            Question = question;
            Options = options;
        }

        public override void HandleDecision(int decisionIndex, string additionalState = "")
        {
            if (Options.Count >= decisionIndex + 1)
            {
                Options[decisionIndex].MetricsModifier.Modify();
                Feedback = Options[decisionIndex].Feedback;
                FeedbackNPCName = Options[decisionIndex].FeedbackNPCName;
                NextStateId = Options[decisionIndex].NextStateId + additionalState;
                Debug.Log("next state id: " + NextStateId);
                ShouldAnimate = Options[decisionIndex].HasAnimation;
                BuildingName = Options[decisionIndex].BuildingName;
            }
        }
    }
}