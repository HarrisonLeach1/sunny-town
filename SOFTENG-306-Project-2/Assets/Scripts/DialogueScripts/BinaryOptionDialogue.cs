using UnityEngine;

namespace SunnyTown
{
    /// <summary>
    /// A BinaryOptionDialogue represents the dialogue displayed to the user
    /// for a binary option.
    /// </summary>
    [System.Serializable]
    public class BinaryOptionDialogue
    {
        [SerializeField] private SimpleDialogue precedingDialogue;
        [SerializeField] private string question;
        [SerializeField] private string option1;
        [SerializeField] private string option2;
        public string Question => question;
        public string Option1 => option1;
        public string Option2 => option2;
        public SimpleDialogue PrecedingDialogue => precedingDialogue;

        public BinaryOptionDialogue(string question, string option1, string option2, SimpleDialogue dialogue)
        {
            this.precedingDialogue = dialogue;
            this.question = question;
            this.option1 = option1;
            this.option2 = option2;
        }

    }
}