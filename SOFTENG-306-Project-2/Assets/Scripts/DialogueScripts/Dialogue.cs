using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField]
    private string name;
    [SerializeField, TextArea(3, 10)]
    private string[] statements;

    public string Name => name;
    public string[] Statements => statements;

    public Dialogue(string[] statements, string name)
    {
        this.statements = statements;
        this.name = name;
    }
}
