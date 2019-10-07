using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace SunnyTown
{
    /// <summary>
    /// An NPC sprite manager handles the resolution of character names 
    /// into their appropriate sprites on dialogue box views
    /// </summary>
    public class NPCSpriteManager
    {
        public static NPCSpriteManager instance = null;
        public const string NAME_FOR_DEFAULT_SPRITE = "citizen";
        private Dictionary<string, byte[]> spriteDictionary { get; set; }
        private Sprite defaultSprite;

        private NPCSpriteManager()
        {
            spriteDictionary = new Dictionary<string, byte[]>();
            spriteDictionary.Add(NAME_FOR_DEFAULT_SPRITE, File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/Harry.png"));
            spriteDictionary.Add("Arvio", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/Arvio.png"));
            spriteDictionary.Add("James the Dog", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/JamesTheDog.png"));
            spriteDictionary.Add("Allena", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/Allena.png"));
            spriteDictionary.Add("Jimmy Cash", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/JimmyCash.png"));
            spriteDictionary.Add("Hunter Gatberg", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/William.png"));
            spriteDictionary.Add("Cowboy Willy", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/CowboyWilly.png"));
            spriteDictionary.Add("You have mail", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/mail.png"));
            spriteDictionary.Add("Advisory Board", File.ReadAllBytes(Directory.GetCurrentDirectory() + "/Assets/Sprites/Board.jpg"));
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

            Debug.Log("sprite not found in spriteDictionary, returning default sprite");
            return spriteDictionary[NAME_FOR_DEFAULT_SPRITE];
        }

        /// <summary>
        /// Returns the sprite given an NPCs name, if no sprite is found for the given NPC
        /// then the default sprite image is returned.
        /// </summary>
        /// <param name="name">The name of the character to retrieve the sprite for</param>
        /// <returns>The appropriate Sprite for the NPC</returns>
        public Sprite GetSprite(string name)
        {
            if (name == null)
            {
                return null;
            }

            int width = 250;
            int height = 250;
            Debug.Log("Getting sprite for: " + name);
            byte[] bytes = this.GetSpriteByteArray(name);
            Texture2D texture = new Texture2D(width, height);
            texture.filterMode = FilterMode.Trilinear;
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.0f), 1.0f);
            return sprite;
        }
    }
}