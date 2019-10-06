using UnityEngine;

namespace SunnyTown
{
    /// <summary>
    /// A SimpleDialogue represents a dto to be interpreted by the DialogueManager 
    /// to interpret and display views from.
    /// </summary>
    [System.Serializable]
    public class SimpleDialogue
    {
        [SerializeField] private string name;
        [SerializeField, TextArea(3, 10)] private string[] statements;

        public string Name => name;
        public string[] Statements => statements;

        public SimpleDialogue(string[] statements, string name)
        {
            this.statements = statements;
            this.name = name;
        }
    }
}
