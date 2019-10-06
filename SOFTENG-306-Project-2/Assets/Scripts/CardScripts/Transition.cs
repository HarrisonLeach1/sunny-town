using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    /// <summary>
    /// A Transition represents the posible outcomes that a decision on a Card
    /// can have, this includes the next state, and feedback
    /// </summary>
    public class Transition
    {
        private string nextStateId;
        public string Dialogue { get; set; }
        public string Feedback { get; set; }
        public string FeedbackNPCName { get; set; }

        public string NextStateId
        {
            get => nextStateId;
            set => nextStateId = value;
        }

        public bool HasAnimation { get; protected set; } = false;
        public string BuildingName { get; set; } = "";
        public MetricsModifier MetricsModifier { get; set; }

        public Transition(string feedback, string feedbackNpcName, MetricsModifier metricsmodifier, bool hasAnimation,
            string buildingName, string dialogue = "", string nextStateId = "")
        {
            this.Dialogue = dialogue;
            this.Feedback = feedback;
            this.FeedbackNPCName = feedbackNpcName;
            this.HasAnimation = hasAnimation;
            this.BuildingName = buildingName;
            this.MetricsModifier = metricsmodifier;
            this.NextStateId = nextStateId;
        }
    }
}