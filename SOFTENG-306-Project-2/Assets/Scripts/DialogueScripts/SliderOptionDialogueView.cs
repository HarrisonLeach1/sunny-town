using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderOptionDialogueView
{
    private NPCSpriteManager npcSpriteManager = NPCSpriteManager.Instance;
    public GameObject viewObject;
    public Slider slider;
    public Image npcImage;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcDialogueText;
    public TextMeshProUGUI sliderValueText;
    public Button confirmButton;

    internal void SetContent(SliderOptionDialogue dialogue, Action<int> handleButtonPressed)
    {
        npcImage = GameObject.Find("NPCImage").GetComponent<Image>();
        npcImage.sprite = this.getSprite(dialogue.PrecedingDialogue.Name);
        npcNameText.text = dialogue.PrecedingDialogue.Name;
        npcDialogueText.text = dialogue.Question;
        slider.maxValue = dialogue.MaxValue;
        slider.minValue = dialogue.MinValue;
        slider.value = ( dialogue.MaxValue - dialogue.MinValue ) / 2;
        sliderValueText.text = ((int)slider.value).ToString();
        confirmButton.onClick.AddListener(() => handleButtonPressed((int) Math.Round(slider.value)));
    }
    
    private Sprite getSprite(string name)
    {
        if (name == null)
        {
            return null;
        }
        
        int width = 250;
        int height = 250;
        byte[] bytes = this.npcSpriteManager.GetSpriteByteArray(name);
        //Texture2D texture = new Texture2D(width, height);
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        texture.filterMode = FilterMode.Trilinear;
        texture.LoadImage(bytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0,0,width, height), new Vector2(0.5f,0.0f), 1.0f);
        //Texture2D texture = new Texture2D(900, 900, TextureFormat.RGB24, false);
        return sprite;
    }
}