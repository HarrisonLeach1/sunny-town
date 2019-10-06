using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BinaryOptionDialogueView
{
    private NPCSpriteManager npcSpriteManager = NPCSpriteManager.Instance;
    public GameObject viewObject;
    public Image image;
    public Image npcImage;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcDialogueText;
    public Button option1Button;
    public Button option2Button;
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;

    internal void SetContent(BinaryOptionDialogue dialogue, Action<int> onOptionPressed)
    {
        npcImage = GameObject.Find("BinaryNPCImage").GetComponent<Image>();
        Debug.Log("Binary NPC Name: " + dialogue.PrecedingDialogue.Name);
        npcImage.sprite = this.npcSpriteManager.getSprite(dialogue.PrecedingDialogue.Name);
        //npcImage.sprite = this.getSprite(dialogue.PrecedingDialogue.Name);
        npcNameText.text = dialogue.PrecedingDialogue.Name;
        npcDialogueText.text = dialogue.Question;
        option1Text.text = dialogue.Option1;
        option2Text.text = dialogue.Option2;
        option1Button.onClick.AddListener(() => onOptionPressed(0));
        option2Button.onClick.AddListener(() => onOptionPressed(1));
    }

//    private Sprite getSprite(string name)
//    {
//        if (name == null)
//        {
//            return null;
//        }
//        
//        int width = 250;
//        int height = 250;
//        byte[] bytes = this.npcSpriteManager.GetSpriteByteArray(name);
//        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
//        //Texture2D texture = new Texture2D(width, height);
//        texture.filterMode = FilterMode.Trilinear;
//        texture.LoadImage(bytes);
//        Sprite sprite = Sprite.Create(texture, new Rect(0,0,width, height), new Vector2(0.5f,0.0f), 1.0f);
//        
//        return sprite;
//    }

}