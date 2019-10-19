using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
    /// <summary>
    /// A OptionDialogueView represents the view on which a multiple choice
    /// decision is displayed to the user
    /// </summary>
    [System.Serializable]
    public class OptionDialogueView
    {
        public GameObject viewObject;
        public Image image;
        public Image npcImage;
        public TextMeshProUGUI npcNameText;
        public TextMeshProUGUI npcDialogueText;
        public Button[] optionButtons;
        public TextMeshProUGUI[] optionTexts;
        public TextMeshProUGUI cardTypeText;



        /// <summary>
        /// Sets the content to be displayed on the view
        /// </summary>
        /// <param name="dialogue">The dialogue to be displayed to the user</param>
        /// <param name="onOptionPressed">A callback which should be used to execute instructions
        /// after a user has chosen an option</param>
        internal void SetContent(OptionDialogue dialogue, Action<int> onOptionPressed)
        {
            // clear current listeners and text on the buttons
            foreach (Button button in optionButtons) {
                button.onClick.RemoveAllListeners();
                button.gameObject.SetActive(false);
            }

            npcImage = GameObject.Find("BinaryNPCImage").GetComponent<Image>();
            Debug.Log("Binary NPC Name: " + dialogue.PrecedingDialogue.Name);
            npcImage.sprite = NPCSpriteManager.Instance.GetSprite(dialogue.PrecedingDialogue.Name);
            npcNameText.text = dialogue.PrecedingDialogue.Name;
            npcDialogueText.text = dialogue.Question;

            // Add buttons for each option available
            foreach (var item in dialogue.Options.Select((value, index) => (value, index)))
            {
                var optionText = item.value;
                // currently 2 is used to render the bottom button first
                var buttonIndex = item.index;
                Button buttonToDisplay = optionButtons[buttonIndex];
                buttonToDisplay.gameObject.SetActive(true);
                optionTexts[buttonIndex].text = optionText;
                buttonToDisplay.onClick.AddListener(() => onOptionPressed(item.index));
            }

            if (dialogue.Options.Count() > 1)
            {
                cardTypeText.text = dialogue.CardType + " Decision";
                cardTypeText.color = dialogue.CardType == "Story" ? new Color32(168, 24, 36, 255) : new Color32(219, 164, 44, 255);
            } 
            else
            {
                cardTypeText.text = "";
            }
        }

    }
}