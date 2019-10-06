using UnityEngine;
using UnityEditor;

public class SliderTransition : Transition
{
    private int threshold;
    public int Threshold => threshold;
    public SliderTransition(string feedback, string feedbackNPCName, MetricsModifier metricsmodifier, bool hasAnimation, string buildingName, int threshold) : base(feedback, feedbackNPCName, metricsmodifier, hasAnimation, buildingName)
    {
        this.threshold = threshold;
    }
}