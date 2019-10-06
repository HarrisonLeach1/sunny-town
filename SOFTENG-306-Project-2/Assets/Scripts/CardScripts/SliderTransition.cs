using UnityEngine;
using UnityEditor;

public class SliderTransition : Transition
{
    private int threshold;
    public int Threshold => threshold;
    public SliderTransition(string feedback, MetricsModifier metricsmodifier, int threshold) : base(feedback, metricsmodifier)
    {
        this.threshold = threshold;
    }
}