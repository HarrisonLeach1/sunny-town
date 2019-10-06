using UnityEngine;
using UnityEditor;

public class SliderTransition : Transition
{
    private int threshold;
    public int Threshold => threshold;
    public SliderTransition(string feedback, MetricsModifier metricsmodifier, bool hasAnimation, string buildingName, int threshold) : base(feedback, metricsmodifier, hasAnimation, buildingName)
    {
        this.threshold = threshold;
    }
}