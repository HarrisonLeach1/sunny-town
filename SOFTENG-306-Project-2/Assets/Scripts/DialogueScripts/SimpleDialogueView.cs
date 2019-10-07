using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
[System.Serializable]
public class SimpleDialogueView
{
    private NPCSpriteManager npcSpriteManager = NPCSpriteManager.Instance;
    public Image image;
    private Image npcImage;
    public GameObject viewObject;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcDialogueText;
    public TextMeshProUGUI buttonText;

    public void SetContent(SimpleDialogue dialogue)
    {
        npcImage = GameObject.Find("SimpleNPCImage").GetComponent<Image>();
        Debug.Log("Simple NPC Name: " + dialogue.Name);
        npcImage.sprite = this.npcSpriteManager.getSprite(dialogue.Name);
    }
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
}