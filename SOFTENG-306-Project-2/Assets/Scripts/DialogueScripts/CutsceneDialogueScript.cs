using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutsceneDialogueScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    private Action onEndOfStatements;
    private Queue<string> statements = new Queue<string>();
    /// <summary>
    /// Displays the next statement in a simple dialogue, and animates its writing
    /// </summary>
    public void DisplayNextStatement()
    {
        if (statements.Count == 0)
        {
            onEndOfStatements();
            return;
        }
        var statement = statements.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(statement));
    }

    public void StartCutsceneDialogue(string[] dialogue, Action onEndOfStatement)
    {
        this.onEndOfStatements = onEndOfStatement;
        this.statements.Clear();

        foreach (string statement in dialogue)
        {
            statements.Enqueue(statement);
        }

        DisplayNextStatement();
    }

    private IEnumerator TypeSentence(string statement)
    {
        dialogueText.text = "";
        foreach (char letter in statement.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

    }
}
