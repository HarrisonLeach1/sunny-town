
public class Achievement
{
    public string name { get; set; }
    public string description { get; set; }
    public string imageUrl { get; set; }
    public string dateEarned { get; set; }

    public Achievement(string name, string description, string imageUrl)
    {
        this.name = name;
        this.description = description;
        this.imageUrl = imageUrl;
    }
}
