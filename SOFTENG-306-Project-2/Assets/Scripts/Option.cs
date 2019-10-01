using UnityEngine;
using UnityEditor;

public class Option
{
    public string Dialogue { get; private set; }

    public Option(string dialogue)
    {
        Dialogue = dialogue;
    }
}