using System.Collections.Generic;

public class NPCSpriteManager
{
    public static NPCSpriteManager instance = null;
//    private Dictionary<string, System.Drawing.Image> spriteDictionary { get; set; }

    private NPCSpriteManager()
    {
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
}