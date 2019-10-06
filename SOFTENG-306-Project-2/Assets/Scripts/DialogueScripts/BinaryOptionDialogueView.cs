using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
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