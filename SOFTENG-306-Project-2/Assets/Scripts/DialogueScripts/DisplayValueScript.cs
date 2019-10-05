using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderOptionDialogueView
{
    public GameObject viewObject;
    public Slider slider;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcDialogueText;
    public TextMeshProUGUI sliderValueText;
    public Button confirmButton;

    internal void SetContent(SliderOptionDialogue dialogue, Action<int> handleButtonPressed)
    {
        npcNameText.text = dialogue.PrecedingDialogue.Name;
        npcDialogueText.text = dialogue.Question;
        slider.maxValue = dialogue.MinValue;
        slider.minValue = dialogue.MaxValue;
        confirmButton.onClick.AddListener(() => handleButtonPressed((int) Math.Round(slider.value)));
    }
}