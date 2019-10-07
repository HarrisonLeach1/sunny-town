using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
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