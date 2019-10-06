using UnityEngine;
using UnityEditor;

namespace SunnyTown
{
    /// <summary>
    /// A SliderTransition represents a the outcome that a decision
    /// in a range of values can have
    /// </summary>
    public class SliderTransition : Transition
    {
        private int threshold;
        public int Threshold => threshold;

        public SliderTransition(string feedback, string feedbackNPCName, MetricsModifier metricsmodifier,
            bool hasAnimation, string buildingName, int threshold) : base(feedback, feedbackNPCName, metricsmodifier,
            hasAnimation, buildingName)
        {
            this.threshold = threshold;
        }
    }
}