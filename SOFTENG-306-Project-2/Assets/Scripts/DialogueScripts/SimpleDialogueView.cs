using System.Collections;
using TMPro;
using UnityEngine;

[System.Serializable]
public class SimpleDialogueView
{
    public GameObject viewObject;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcDialogueText;

    public IEnumerator TypeSentence(string statement)
    {
        npcDialogueText.text = "";
        foreach (char letter in statement.ToCharArray())
        {
            npcDialogueText.text += letter;
            yield return null;
        }
    }
}