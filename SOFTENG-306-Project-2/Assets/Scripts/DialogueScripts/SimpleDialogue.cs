using UnityEngine;

namespace SunnyTown
{
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
