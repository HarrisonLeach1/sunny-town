using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Initial Dialogue implementation based on code from: 
// https://github.com/Brackeys/Dialogue-System
public class DialogueManager : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcDialogueText;
    public static DialogueManager Instance { get; private set; }
    private Queue<string> statements = new Queue<string>();

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

    public void StartSimpleDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsVisible", true);
        statements.Clear();

        npcNameText.text = dialogue.Name;
        foreach (string statement in dialogue.Statements)
        {
            statements.Enqueue(statement);
        }

        DisplayNextStatement();
    }

    public void StartBinaryOptionDialogue(BinaryOptionDialogue dialogue)
    {
        Debug.Log(dialogue.Option1 + dialogue.Option2);
    }

    public void DisplayNextStatement()
    {
        if (statements.Count == 0)
        {
            EndDialogue();
            return;
        }
        var statement = statements.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(statement));
    }

    IEnumerator TypeSentence(string statement)
    {
        npcDialogueText.text = "";
        foreach (char letter in statement.ToCharArray())
        {
            npcDialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        animator.SetBool("IsVisible", false);
        CardManager.Instance.StartDisplayingCards();
    }
}
