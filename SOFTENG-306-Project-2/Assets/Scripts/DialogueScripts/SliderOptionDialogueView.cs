using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
[System.Serializable]
public class SliderOptionDialogueView
{
    private NPCSpriteManager npcSpriteManager = NPCSpriteManager.Instance;
    public GameObject viewObject;
    public Slider slider;
    public Image npcImage;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcDialogueText;
    public TextMeshProUGUI sliderValueText;
    public Button confirmButton;

    internal void SetContent(SliderOptionDialogue dialogue, Action<int> handleButtonPressed)
    {
        confirmButton.onClick.RemoveAllListeners();

        npcImage = GameObject.Find("SliderNPCImage").GetComponent<Image>();
        Debug.Log("Slider NPC Name: " + dialogue.PrecedingDialogue.Name);
        npcImage.sprite = this.npcSpriteManager.GetSprite(dialogue.PrecedingDialogue.Name);
        //npcImage.sprite = this.getSprite(dialogue.PrecedingDialogue.Name);
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
}