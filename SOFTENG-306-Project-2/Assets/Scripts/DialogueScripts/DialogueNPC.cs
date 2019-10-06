using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    //public SimpleDialogue dialogue;
    private Reader reader;
    private List<SimpleDialogue> expositionDialogues;
    private SimpleDialogue currentExpositionDialogue;

    private void Start()
    {
        this.reader = new Reader();
        this.expositionDialogues = reader.AllExpositionDialogues;
//        this.currentExpositionDialogue = this.expositionDialogues[0];
//        this.expositionDialogues.RemoveAt(0);
        Debug.Log("Number of exposition states: " +this.expositionDialogues.Count);
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        Action handleDialogueClosed = () => this.TriggerDialogue();
        if (this.expositionDialogues.Count == 1)
        {
            handleDialogueClosed = () => CardManager.Instance.StartDisplayingCards();
        }
        
        this.currentExpositionDialogue = this.expositionDialogues[0];
        this.expositionDialogues.RemoveAt(0);
        
        DialogueManager.Instance.StartExplanatoryDialogue(this.currentExpositionDialogue, handleDialogueClosed);
    }
    
    
//    public void TriggerDialogue()
//    {
////        Action handleDialogueClosed = null;
//        Action handleDialogueClosed = () => CardManager.Instance.StartDisplayingCards();
//        foreach (SimpleDialogue dialogue in this.expositionDialogues)
//        {
//            DialogueManager.Instance.StartExplanatoryDialogue(dialogue, handleDialogueClosed);
//        }
////        for (int i = 0; i < this.expositionDialogues.Count; i++)
////        {
////            if (i == this.expositionDialogues.Count - 1)
////            {
////                handleDialogueClosed = () => CardManager.Instance.StartDisplayingCards();
////            }
////
////            DialogueManager.Instance.StartExplanatoryDialogue(this.expositionDialogues[i], handleDialogueClosed);
////        }
//    }
    
}
