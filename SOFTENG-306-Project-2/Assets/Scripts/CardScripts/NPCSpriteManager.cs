using System.IO;
//using UnityEngine;
//using UnityEngine.UI;
using System.Collections.Generic;

public class NPCSpriteManager
{
    public static NPCSpriteManager instance = null;
    private Dictionary<string, byte[]> spriteDictionary { get; set; }

    private NPCSpriteManager()
    {
        spriteDictionary = new Dictionary<string, byte[]>();
        spriteDictionary.Add("Arvio", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/Arvio.png"));
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
        return spriteDictionary[name];
    }
}