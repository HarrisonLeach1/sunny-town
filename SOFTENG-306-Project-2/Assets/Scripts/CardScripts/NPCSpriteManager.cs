using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NPCSpriteManager
{
    public static NPCSpriteManager instance = null;
    private Dictionary<string, byte[]> spriteDictionary { get; set; }

    private NPCSpriteManager()
    {
        spriteDictionary = new Dictionary<string, byte[]>();
        spriteDictionary.Add("Arvio", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/Arvio.png"));
        spriteDictionary.Add("James the Dog", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/JamesTheDog.png"));
        spriteDictionary.Add("Allena", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/Allena.png"));
        spriteDictionary.Add("Jimmy Cash", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/JimmyCash.png"));
//        spriteDictionary.Add("square", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/Square.png"));
//        spriteDictionary.Add("", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/Square.png"));
//        spriteDictionary.Add("Board of Advisors", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/Square.png"));
    }

    public static NPCSpriteManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NPCSpriteManager();
            }

            return instance;
        }
    }

    public byte[] GetSpriteByteArray(string name)
    {
        if (spriteDictionary.ContainsKey(name))
        {
            return spriteDictionary[name];
        }

        Debug.Log("sprite not found in spriteDictionary");
        return null;
    }
    
    public Sprite getSprite(string name)
    {
        if (name == null)
        {
            return null;
        }
        
        int width = 250;
        int height = 250;
        Debug.Log("Getting sprite for: " + name);
        byte[] bytes = this.GetSpriteByteArray(name);
        
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