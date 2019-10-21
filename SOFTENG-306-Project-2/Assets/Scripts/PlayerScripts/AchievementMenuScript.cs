using UnityEngine;

/// <summary>
/// An achievement menu script is used to update the Achievement of relevant events that 
/// should be performed in this scene.
/// </summary>
public class AchievementMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AchievementsManager.Instance.DisplayHighScores();
        AchievementsManager.Instance.DisplayAchievementsMenu();
    }
}
