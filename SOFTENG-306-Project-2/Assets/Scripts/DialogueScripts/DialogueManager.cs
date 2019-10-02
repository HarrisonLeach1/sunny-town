using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcDialogueText;
    public static DialogueManager Instance { get; private set; }
    private Queue<string> statements = new Queue<string>();

    public void StartDialogue(Dialogue dialogue)
    {
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
        throw new NotImplementedException();
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
