using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BinaryOptionDialogueView
{
    public GameObject viewObject;
    public Image image;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcDialogueText;
    public Button option1Button;
    public Button option2Button;
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;

    internal void SetContent(BinaryOptionDialogue dialogue, Action<int> onOptionPressed)
    {
        npcNameText.text = dialogue.PrecedingDialogue.Name;
        npcDialogueText.text = dialogue.Question;
        option1Text.text = dialogue.Option1;
        option2Text.text = dialogue.Option2;
        option1Button.onClick.AddListener(() => onOptionPressed(0));
        option2Button.onClick.AddListener(() => onOptionPressed(1));
    }
}