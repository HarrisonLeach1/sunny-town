using UnityEngine;

namespace SunnyTown
{
    /// <summary>
    /// A BinaryOptionDialogue represents the dialogue displayed to the user
    /// for a binary option.
    /// </summary>
    [System.Serializable]
    public class OptionDialogue
    {
        [SerializeField] private SimpleDialogue precedingDialogue;
        [SerializeField] private string question;
        [SerializeField] private string[] options;
        [SerializeField] private string cardType;

        public string Question => question;
        public string[] Options => options;
        public SimpleDialogue PrecedingDialogue => precedingDialogue;
        public string CardType => cardType;

        public OptionDialogue(string question, string[] options, SimpleDialogue dialogue, string cardType)
        {
            this.precedingDialogue = dialogue;
            this.question = question;
            this.options = options;
            this.cardType = cardType;
        }

    }
}