using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class DialogueNPC : MonoBehaviour
    {
        private Reader reader;
        private List<SimpleDialogue> expositionDialogues;
        private SimpleDialogue currentExpositionDialogue;

        private void Start()
        {
            this.reader = new Reader();
            this.expositionDialogues = reader.AllExpositionDialogues;
            Debug.Log("Number of exposition states: " + this.expositionDialogues.Count);
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
    }
}
