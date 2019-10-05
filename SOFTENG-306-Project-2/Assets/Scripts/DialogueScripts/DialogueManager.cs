using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Initial Dialogue implementation based on code from: 
// https://github.com/Brackeys/Dialogue-System
public class DialogueManager : MonoBehaviour
{ 
    public Animator simpleDialogueViewAnimator;
    public Animator binaryOptionViewAnimator;
    public SimpleDialogueView simpleDialogueView;
    public BinaryOptionDialogueView binaryOptionDialogueView;
    public static DialogueManager Instance { get; private set; }
    private Queue<string> statements = new Queue<string>();
    private Action onEndOfStatements;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Displays purely explanatory dialogue. This means the only interactivity available is
    /// pressing the "Continue" button. i.e. no decisions to be made.
    /// </summary>
    /// <param name="dialogue"></param>
    /// <param name="onClosed"></param>
    public void StartExplanatoryDialogue(SimpleDialogue dialogue, Action onClosed)
    {
        Action onEndOfStatements = () =>
        {
            simpleDialogueViewAnimator.SetTrigger("ToggleVisibilitySmooth");
            onClosed();
        };
        StartSimpleDialogue(dialogue, onEndOfStatements);
    }

    public void StartBinaryOptionDialogue(BinaryOptionDialogue dialogue, Action<int> onOptionPressed)
    {
        Action<int> handleButtonPressed = num =>
        {
            onOptionPressed(num);
            binaryOptionViewAnimator.SetTrigger("ToggleVisibilitySmooth");
        };

        if (dialogue.PrecedingDialogue.Statements.Length != 0)
        {
            StartSimpleDialogue(dialogue.PrecedingDialogue, () => {
                binaryOptionDialogueView.SetContent(dialogue, handleButtonPressed);
                simpleDialogueViewAnimator.SetTrigger("ToggleVisibilityInstant");
                binaryOptionViewAnimator.SetTrigger("ToggleVisibilityInstant");
            });
        }
        else
        {
            binaryOptionDialogueView.SetContent(dialogue, handleButtonPressed);
            binaryOptionViewAnimator.SetTrigger("ToggleVisibilitySmooth");
        }
    }

    public void DisplayNextStatement()
    {
        if (statements.Count == 0)
        {
            onEndOfStatements();
            return;
        }
        var statement = statements.Dequeue();
        StopAllCoroutines();
        StartCoroutine(simpleDialogueView.TypeSentence(statement));
    }

    private void StartSimpleDialogue(SimpleDialogue dialogue, Action onEndOfStatements)
    {
        this.onEndOfStatements = onEndOfStatements;
        simpleDialogueViewAnimator.SetTrigger("ToggleVisibilitySmooth");
        statements.Clear();

        simpleDialogueView.npcNameText.text = dialogue.Name;
        foreach (string statement in dialogue.Statements)
        {
            statements.Enqueue(statement);
        }

        DisplayNextStatement();
    }
}
