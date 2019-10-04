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
        
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsVisible", true);
        Debug.Log("Started: " + dialogue.name);
        statements.Clear();

        npcNameText.text = dialogue.name;
        foreach (string statement in dialogue.statements)
        {
            statements.Enqueue(statement);
        }

        DisplayNextStatement();
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

    IEnumerator TypeSentence (string statement)
    {
        npcDialogueText.text = "";
        foreach(char letter in statement.ToCharArray())
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
