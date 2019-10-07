using UnityEngine;

namespace SunnyTown
{
 
    /// <summary>
    /// A SliderOptionDialogue represents the dialogue that allows the user to
    /// choose a quantity. The quantity can be used to change the metrics depending on
    /// the threshold. The user only has 1 option that typically
    /// is to 'continue' to the next dialogue
    /// </summary>
    [System.Serializable]
    public class SliderOptionDialogue
    {
        [SerializeField] private SimpleDialogue precedingDialogue;
        [SerializeField] private string question;
        [SerializeField] private int maxValue;
        [SerializeField] private int minValue;
        public string Question => question;
        public SimpleDialogue PrecedingDialogue => precedingDialogue;
        public int MaxValue => maxValue;
        public int MinValue => minValue;

        public SliderOptionDialogue(string question, int maxValue, int minValue, SimpleDialogue dialogue)
        {
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.precedingDialogue = dialogue;
            this.question = question;
        }

    }
}