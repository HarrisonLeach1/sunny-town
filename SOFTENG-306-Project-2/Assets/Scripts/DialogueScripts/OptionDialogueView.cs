using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
    /// <summary>
    /// A BinaryOptionDialogueView represents the view on which a binary
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

            foreach(string optionDialogue in dialogue.Options)
            option1Button.onClick.AddListener(() => onOptionPressed(0));
            option2Button.onClick.AddListener(() => onOptionPressed(1));
        }

    }
}