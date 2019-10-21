using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
    /// <summary>
    /// A SliderOptionDialogueView represents the view on which a slider option
    /// decision is displayed to the user
    /// </summary>
    [System.Serializable]
    public class SliderOptionDialogueView
    {
        public GameObject viewObject;
        public Slider slider;
        public Image npcImage;
        public TextMeshProUGUI npcNameText;
        public TextMeshProUGUI npcDialogueText;
        public TextMeshProUGUI sliderValueText;
        public Button confirmButton;
        public TextMeshProUGUI cardTypeText;

        /// <summary>
        /// Sets the content of the dialogue to be displayed to the user
        /// </summary>
        /// <param name="dialogue">the SliderOptionDialogue to render contents from</param>
        /// <param name="handleButtonPressed">A callback which is to be called when the button is pressed</param>
        internal void SetContent(SliderOptionDialogue dialogue, Action<int> handleButtonPressed)
        {
            confirmButton.onClick.RemoveAllListeners();

            npcImage = GameObject.Find("SliderNPCImage").GetComponent<Image>();
            Debug.Log("Slider NPC Name: " + dialogue.PrecedingDialogue.Name);
            npcImage.sprite = NPCSpriteManager.Instance.GetSprite(dialogue.PrecedingDialogue.Name);
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

            cardTypeText.text = dialogue.CardType + " Decision";
            cardTypeText.color = dialogue.CardType == "Story" ? new Color32(168, 24, 36, 255) : new Color32(219, 164, 44, 255);
        }
    }
}