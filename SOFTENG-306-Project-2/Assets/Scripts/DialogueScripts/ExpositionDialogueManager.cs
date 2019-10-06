using System;
using UnityEngine;

public class ExpositionDialogueManager : MonoBehaviour
{
    public SimpleDialogue dialogue;

    private void Start()
    {
        TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        Action handleDialogueClosed = () => CardManager.Instance.StartDisplayingCards();
        DialogueManager.Instance.StartExplanatoryDialogue(dialogue, handleDialogueClosed);
    }
}