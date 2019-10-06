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
    public Animator sliderOptionViewAnimator;
    public SimpleDialogueView simpleDialogueView;
    public BinaryOptionDialogueView binaryOptionDialogueView;
    public SliderOptionDialogueView sliderOptionDialogueView;
    public static DialogueManager Instance { get; private set; }
    private Queue<string> statements = new Queue<string>();
    private Action onEndOfStatements;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        Action onEndOfStatement = () =>
        {
            simpleDialogueViewAnimator.SetBool("InstantTransition", false);
            simpleDialogueViewAnimator.SetBool("IsVisible", false);
            onClosed();
        };
        StartSimpleDialogue(dialogue, onEndOfStatement);
//        StartSimpleDialogue(dialogue, onClosed);
    }

    public void StartBinaryOptionDialogue(BinaryOptionDialogue dialogue, Action<int> onOptionPressed)
    {
        Action<int> handleButtonPressed = num =>
        {
            onOptionPressed(num);
            binaryOptionViewAnimator.SetBool("InstantTransition", false);
            binaryOptionViewAnimator.SetBool("IsVisible", false);
        };

        if (dialogue.PrecedingDialogue.Statements.Length != 0)
        {
            StartSimpleDialogue(dialogue.PrecedingDialogue, () => {
                binaryOptionDialogueView.SetContent(dialogue, handleButtonPressed);
                simpleDialogueViewAnimator.SetBool("InstantTransition", true);
                simpleDialogueViewAnimator.SetBool("IsVisible", false);
                binaryOptionViewAnimator.SetBool("InstantTransition", true);
                binaryOptionViewAnimator.SetBool("IsVisible", true);
            });
        }
        else
        {
            binaryOptionDialogueView.SetContent(dialogue, handleButtonPressed);
            binaryOptionViewAnimator.SetBool("InstantTransition", false);
            binaryOptionViewAnimator.SetBool("IsVisible", false);
        }
    }

    public void StartSliderOptionDialogue(SliderOptionDialogue dialogue, Action<int> onValueConfirmed)
    {
        Action<int> handleButtonPressed = value =>
        {
            onValueConfirmed(value);
            sliderOptionViewAnimator.SetTrigger("ToggleVisibilitySmooth");
        };

        if (dialogue.PrecedingDialogue.Statements.Length != 0)
        {
            StartSimpleDialogue(dialogue.PrecedingDialogue, () => {
                sliderOptionDialogueView.SetContent(dialogue, handleButtonPressed);
                simpleDialogueViewAnimator.SetBool("InstantTransition", true);
                simpleDialogueViewAnimator.SetBool("IsVisible", false);
                sliderOptionViewAnimator.SetBool("InstantTransition", true);
                sliderOptionViewAnimator.SetBool("IsVisible", true);
            });
        }
        else
        {
            sliderOptionDialogueView.SetContent(dialogue, handleButtonPressed);
            sliderOptionViewAnimator.SetBool("InstantTransition", false);
            sliderOptionViewAnimator.SetBool("IsVisible", false);
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

    private void StartSimpleDialogue(SimpleDialogue dialogue, Action onEndOfStatement)
    {
        simpleDialogueView.SetContent(dialogue);
        this.onEndOfStatements = onEndOfStatement;
        simpleDialogueViewAnimator.SetBool("InstantTransition", false);
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
