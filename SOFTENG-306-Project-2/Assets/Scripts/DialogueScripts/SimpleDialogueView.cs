using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        //npcImage.sprite = this.getSprite(dialogue.Name);
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
    
    private Sprite getSprite(string name)
    {
        if (name == null)
        {
            return null;
        }
        
        int width = 250;
        int height = 250;
        Debug.Log("Getting sprite for: " + name);
        byte[] bytes = this.npcSpriteManager.GetSpriteByteArray(name);
        if (bytes == null)
        {
            Debug.Log("bytes[] is null");
            return null;
        }
        Texture2D texture = new Texture2D(width, height);
        //Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        texture.filterMode = FilterMode.Trilinear;
        texture.LoadImage(bytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0,0,width, height), new Vector2(0.5f,0.0f), 1.0f);
        //Sprite sprite = Sprite.Create(texture, new Rect(0,0,width, height), new Vector2(0.0f,0.0f), 1.0f);
        return sprite;
    }
}