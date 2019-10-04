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
        
        if (dialogue.LeadingDialogue.Statements.Length != 0)
        {
            StartSimpleDialogue(dialogue.LeadingDialogue, () => DisplayBinaryOptionDialogue(dialogue, onOptionPressed));
        }
        else
        {
            DisplayBinaryOptionDialogue(dialogue, onOptionPressed);
        }
    }


    private void DisplayBinaryOptionDialogue(BinaryOptionDialogue dialogue, Action<int> onOptionPressed)
    {
        binaryOptionDialogueView.npcNameText.text = dialogue.LeadingDialogue.Name;
        binaryOptionDialogueView.npcDialogueText.text = dialogue.Question;
        binaryOptionDialogueView.option1Text.text = dialogue.Option1;
        binaryOptionDialogueView.option2Text.text = dialogue.Option2;
        binaryOptionDialogueView.option1Button.onClick.AddListener(() => onOptionPressed(0));
        binaryOptionDialogueView.option1Button.onClick.AddListener(() => onOptionPressed(1));
        Destroy(simpleDialogueView.viewObject);
        binaryOptionDialogueView.viewObject.transform.position = simpleDialogueView.viewObject.transform.position;
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
        StartCoroutine(TypeSentence(statement));
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

    private IEnumerator TypeSentence(string statement)
    {
        simpleDialogueView.npcDialogueText.text = "";
        foreach (char letter in statement.ToCharArray())
        {
            simpleDialogueView.npcDialogueText.text += letter;
            yield return null;
        }
    }
}
