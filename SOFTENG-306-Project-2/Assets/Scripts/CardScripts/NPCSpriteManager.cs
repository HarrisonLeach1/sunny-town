using UnityEngine;
using System.Collections.Generic;

namespace SunnyTown
{
    /// <summary>
    /// An NPC sprite manager handles the resolution of character names 
    /// into their appropriate sprites on dialogue box views
    /// </summary>
    public class NPCSpriteManager : MonoBehaviour
    {
        public static NPCSpriteManager Instance { get; private set; }
        public const string NAME_FOR_DEFAULT_SPRITE = "citizen";
        private Dictionary<string, Texture2D> spriteDictionary { get; set; }
        private Texture2D defaultSprite;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            spriteDictionary = new Dictionary<string, Texture2D>();
            spriteDictionary.Add(NAME_FOR_DEFAULT_SPRITE, Resources.Load<Texture2D>("Sprites/Harry"));
            spriteDictionary.Add("Arvio", Resources.Load<Texture2D>("Sprites/Arvio"));
            spriteDictionary.Add("James the Dog", Resources.Load<Texture2D>("Sprites/JamesTheDog"));
            spriteDictionary.Add("Allena", Resources.Load<Texture2D>("Sprites/Allena"));
            spriteDictionary.Add("Jimmy Cash", Resources.Load<Texture2D>("Sprites/JimmyCash"));
            spriteDictionary.Add("Hunter Gatberg", Resources.Load<Texture2D>("Sprites/William"));
            spriteDictionary.Add("Cowboy Willy", Resources.Load<Texture2D>("Sprites/CowboyWilly"));
            spriteDictionary.Add("You have mail", Resources.Load<Texture2D>("Sprites/mail"));
            spriteDictionary.Add("Advisory Board", Resources.Load<Texture2D>("Sprites/Board"));
            spriteDictionary.Add("Weather event", Resources.Load<Texture2D>("Sprites/weather"));
            spriteDictionary.Add("First Plebeian", Resources.Load<Texture2D>("Sprites/Pleb1"));
            spriteDictionary.Add("Second Plebeian", Resources.Load<Texture2D>("Sprites/Pleb2"));
            spriteDictionary.Add("Third Plebeian", Resources.Load<Texture2D>("Sprites/Pleb3"));
            spriteDictionary.Add("Forth Plebeian", Resources.Load<Texture2D>("Sprites/Pleb4"));
        }

        private Texture2D GetSpriteTexture(string name)
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

            Texture2D texture = this.GetSpriteTexture(name);
            texture.filterMode = FilterMode.Trilinear;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.0f), 1.0f);
            return sprite;
        }
    }
}