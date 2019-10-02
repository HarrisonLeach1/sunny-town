using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcDialogueText;
    public static DialogueManager Instance { get; private set; }
    private Queue<string> statements = new Queue<string>();

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
        npcDialogueText.text = statements.Dequeue();
    }

    private void EndDialogue()
    {
        animator.SetBool("IsVisible", false);
    }

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
}
