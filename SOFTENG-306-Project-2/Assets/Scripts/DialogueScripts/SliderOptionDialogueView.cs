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
        confirmButton.onClick.RemoveAllListeners();

        npcNameText.text = dialogue.PrecedingDialogue.Name;
        npcDialogueText.text = dialogue.Question;
        slider.maxValue = dialogue.MaxValue;
        slider.minValue = dialogue.MinValue;
        slider.value = (dialogue.MaxValue - dialogue.MinValue) / 2;
        sliderValueText.text = ((int)slider.value).ToString();
        confirmButton.onClick.AddListener(() =>
        {
            handleButtonPressed((int)Math.Round(slider.value));
        });
    }
}