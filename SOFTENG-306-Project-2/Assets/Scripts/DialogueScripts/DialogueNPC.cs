using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    //public SimpleDialogue dialogue;
    private Reader reader;
    private List<SimpleDialogue> expositionDialogues;

    private void Start()
    {
        this.reader = new Reader();
        this.expositionDialogues = reader.AllExpositionDialogues;
        TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        Action handleDialogueClosed = () => CardManager.Instance.StartDisplayingCards();
        foreach (SimpleDialogue dialogue in this.expositionDialogues)
        {
            DialogueManager.Instance.StartExplanatoryDialogue(dialogue, handleDialogueClosed);
        }
    }
    
}
