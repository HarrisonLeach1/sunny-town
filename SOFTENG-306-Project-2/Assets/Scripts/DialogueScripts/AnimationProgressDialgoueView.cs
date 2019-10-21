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

        /// <summary>
        /// Sets the max value of the progress bar. This should match the 
        /// wait/animation time this dialogue is displayed for. 
        /// </summary>
        /// <param name="waitSeconds">The max value of the slider, used to indicate wait time</param>
        public void SetupProgressbar(float waitSeconds)
        {
            slider.minValue = 0;
            slider.maxValue = waitSeconds;
        }
    }
}