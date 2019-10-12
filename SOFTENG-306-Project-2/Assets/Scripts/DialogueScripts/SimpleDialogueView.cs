using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
    /// <summary>
    /// A SimpleDialogueView represents the view which is displayed to the user 
    /// given a SimpleDialogue
    /// </summary>
    [System.Serializable]
    public class SimpleDialogueView
    {
        public Image image;
        private Image npcImage;
        public GameObject viewObject;
        public TextMeshProUGUI npcNameText;
        public TextMeshProUGUI npcDialogueText;
        public TextMeshProUGUI buttonText;

        /// <summary>
        /// Sets the contents to be displayed
        /// </summary>
        /// <param name="dialogue">to be displayed</param>
        public void SetContent(SimpleDialogue dialogue)
        {
            npcImage = GameObject.Find("SimpleNPCImage").GetComponent<Image>();
            npcImage.sprite = NPCSpriteManager.Instance.GetSprite(dialogue.Name);
        }
        public IEnumerator TypeSentence(string statement)
        {
            npcDialogueText.text = "";
            foreach (char letter in statement.ToCharArray())
            {
                npcDialogueText.text += letter;
                yield return null;
            }

        }
    }
}