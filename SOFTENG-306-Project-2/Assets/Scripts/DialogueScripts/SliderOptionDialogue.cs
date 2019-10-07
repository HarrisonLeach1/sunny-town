using UnityEngine;

namespace SunnyTown
{
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