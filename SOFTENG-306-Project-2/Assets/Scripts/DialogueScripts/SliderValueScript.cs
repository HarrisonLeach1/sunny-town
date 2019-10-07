using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
    public class SliderValueScript : MonoBehaviour
    {
        public TextMeshProUGUI displayText;
        public Slider slider;

        public void UpdateText()
        {
            displayText.text = ((int) Math.Round(slider.value)).ToString();
        }
    }
}
