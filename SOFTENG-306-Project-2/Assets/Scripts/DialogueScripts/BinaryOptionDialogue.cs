using UnityEngine;

[System.Serializable]
public class BinaryOptionDialogue : Dialogue
{
    [SerializeField]
    private string question;
    [SerializeField]
    private string option1;
    [SerializeField]
    private string option2;
    public string Question => question; 
    public string Option1 => option1;
    public string Option2 => option2;

    public BinaryOptionDialogue(string question, string option1, string option2, string[] statements, string name) : base(statements, name)
    {
        this.question = question;
        this.option1 = option1;
        this.option2 = option2;
    }

}