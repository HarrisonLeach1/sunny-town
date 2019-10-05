using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SimpleDialogueView
{
    public GameObject viewObject;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcDialogueText;
    public TextMeshProUGUI buttonText;

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