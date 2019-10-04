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

    public void StartTutorialDialogue(SimpleDialogue dialogue, Action onClosed)
    {
        Action onEndOfStatements = () =>
        {
            simpleDialogueViewAnimator.SetBool("IsVisible", false);
            onClosed();
        };
        StartSimpleDialogue(dialogue, onEndOfStatements);
    }

    public void StartBinaryOptionDialogue(BinaryOptionDialogue dialogue, Action<int> onOptionPressed)
    {
        Action<int> handleButtonPressed = num =>
        {
            onOptionPressed(num);
            binaryOptionViewAnimator.SetBool("IsVisible", false);
        };

        if (dialogue.LeadingDialogue.Statements.Length != 0)
        {
            StartSimpleDialogue(dialogue.LeadingDialogue, () => { 
                simpleDialogueViewAnimator.SetBool("IsVisible", false);
                binaryOptionViewAnimator.SetBool("IsVisible", true);
                binaryOptionDialogueView.Display(dialogue, handleButtonPressed);
            });
        }
        else
        {
            binaryOptionDialogueView.Display(dialogue, handleButtonPressed);
            binaryOptionViewAnimator.SetBool("IsVisible", true);
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
        simpleDialogueViewAnimator.SetBool("IsVisible", true);
        statements.Clear();

        simpleDialogueView.npcNameText.text = dialogue.Name;
        foreach (string statement in dialogue.Statements)
        {
            statements.Enqueue(statement);
        }

        DisplayNextStatement();
    }
}
