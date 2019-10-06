using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class SliderCard : Card
{
    public string Name { get; set; }
    public new List<SliderTransition> Options { get; private set; }
    public int MaxValue { get; private set; }
    public int MinValue { get; private set; }

    public SliderCard(string[] precedingDialogue, string name, string question, List<SliderTransition> options, int maxValue, int minValue)
    {
        PrecedingDialogue = precedingDialogue;
        Name = name;
        Question = question;
        Options = options;
        MaxValue = maxValue;
        MinValue = minValue;
    }

    public override void HandleDecision(int valueSelected)
    {
        if (Options.Count >= 1)
        {
            SliderTransition optionChosen;
            var optionsAboveThreshold = Options.Where(option => valueSelected > option.Threshold);

            // if none were found to be above the thresholds
            if (optionsAboveThreshold.ToList().Count == 0)
            {
                // Retrieve the minimum
                optionChosen = Options.OrderByDescending(x => x.Threshold).Last();
            }
            else
            {
                // Otherwise select the max one above the thresholds
                optionChosen = optionsAboveThreshold.OrderByDescending(x => x.Threshold).First();
            }

            optionChosen.MetricsModifier.Modify();
            Feedback = optionChosen.Feedback;
            ShouldAnimate = optionChosen.HasAnimation;
        }
    }
}