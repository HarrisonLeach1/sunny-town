using System;
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
    public class BinaryOptionDialogueView
    {
        private NPCSpriteManager npcSpriteManager = NPCSpriteManager.Instance;
        public GameObject viewObject;
        public Image image;
        public Image npcImage;
        public TextMeshProUGUI npcNameText;
        public TextMeshProUGUI npcDialogueText;
        public Button option1Button;
        public Button option2Button;
        public TextMeshProUGUI option1Text;
        public TextMeshProUGUI option2Text;

        /// <summary>
        /// Sets the content to be displayed on the view
        /// </summary>
        /// <param name="dialogue">The dialogue to be displayed to the user</param>
        /// <param name="onOptionPressed">A callback which should be used to execute instructions
        /// after a user has chosen an option</param>
        internal void SetContent(BinaryOptionDialogue dialogue, Action<int> onOptionPressed)
        {
            option1Button.onClick.RemoveAllListeners();
            option2Button.onClick.RemoveAllListeners();

            npcImage = GameObject.Find("BinaryNPCImage").GetComponent<Image>();
            Debug.Log("Binary NPC Name: " + dialogue.PrecedingDialogue.Name);
            npcImage.sprite = this.npcSpriteManager.GetSprite(dialogue.PrecedingDialogue.Name);
            //npcImage.sprite = this.getSprite(dialogue.PrecedingDialogue.Name);
            npcNameText.text = dialogue.PrecedingDialogue.Name;
            npcDialogueText.text = dialogue.Question;
            option1Text.text = dialogue.Option1;
            option2Text.text = dialogue.Option2;
            option1Button.onClick.AddListener(() => onOptionPressed(0));
            option2Button.onClick.AddListener(() => onOptionPressed(1));
        }

    }
}