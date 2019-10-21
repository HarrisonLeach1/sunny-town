using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
    /// <summary>
    /// A SliderValueScript is a simple script used to change the text of a
    /// TextMeshProUGUI object to match the value of the attached slider.
    /// </summary>
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
