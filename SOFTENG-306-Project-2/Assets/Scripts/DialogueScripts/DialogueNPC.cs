using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public Dialogue dialogue;

    private void Start()
    {
        TriggerDialogue();
    }
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartSimpleDialogue(dialogue);
    }
}
