using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// A cutscene dialogue script is used to display dialogue in an unobtrusive way to the user.
/// Directing their attention to focus on the cutscene more.
/// </summary>
/// Initial Dialogue implementation based on code from: 
/// https://github.com/Brackeys/Dialogue-System
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

    /// <summary>
    /// Starts a cutscene dialogue on the screen with the given sentences,
    /// and invokes the given callback when the player finishes the dialogue.
    /// </summary>
    /// <param name="dialogue">The sentences to play in the cutscene, each element of the array taking a 
    /// frame each </param>
    /// <param name="onEndOfStatement"> Callback that is invoked when the user has reached the end of the dialogue</param>
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
