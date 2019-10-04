using System;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public SimpleDialogue dialogue;

    private void Start()
    {
        TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        Action handleDialogueClosed = () => CardManager.Instance.StartDisplayingCards();
        DialogueManager.Instance.StartTutorialDialogue(dialogue, handleDialogueClosed);
    }
}
