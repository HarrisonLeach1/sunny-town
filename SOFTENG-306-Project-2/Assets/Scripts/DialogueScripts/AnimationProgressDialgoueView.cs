using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
    /// <summary>
    /// Represents the dialogue displayed to the user when 
    /// indicating the progress of the animation of buildings
    /// </summary>
    [System.Serializable]
    public class AnimationProgressDialgoueView
    {
        public GameObject viewObject;
        public Slider slider;

        public void SetupProgressbar(float waitSeconds)
        {
            slider.minValue = 0;
            slider.maxValue = waitSeconds;
        }
    }
}