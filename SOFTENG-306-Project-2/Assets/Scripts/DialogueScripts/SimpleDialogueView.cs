using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SimpleDialogueView
{
//    private NPCSpriteManager npcSpriteManager = NPCSpriteManager.Instance;
    public Image image;
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
//        image.sprite = this.getSprite(dialogue.PrecedingDialogue.Name);

    }
    
//    private Sprite getSprite(string name)
//    {
//        if (name == null)
//        {
//            return null;
//        }
//        
//        int width = 200;
//        int height = 200;
//        byte[] bytes = this.npcSpriteManager.GetSpriteByteArray(name);
//        Texture2D texture = new Texture2D(width, height);
//        texture.filterMode = FilterMode.Trilinear;
//        texture.LoadImage(bytes);
//        Sprite sprite = Sprite.Create(texture, new Rect(0,0,width, height), new Vector2(0.5f,0.0f), 1.0f);
//        //Texture2D texture = new Texture2D(900, 900, TextureFormat.RGB24, false);
//        return sprite;
//    }
}