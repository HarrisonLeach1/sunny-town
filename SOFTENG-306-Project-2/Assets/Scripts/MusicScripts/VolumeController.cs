﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace SunnyTown
{
    public class VolumeController : MonoBehaviour
    {
       //public static VolumeController Instance { get; private set; }
       
       public AudioMixer mixer;
       public Slider slider;

//       private VolumeController()
//       {
//          
//       }

       void Start()
       {
          SetLevel();
          //slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.60f);
       }

       public void SetLevel()
       {
          float sliderValue = slider.value;
          //using logarithmic conversion
          //takes 0.001 and 1 value into a value between -80 and 0 on a log scale
          mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
          PlayerPrefs.SetFloat("MusicVolume", sliderValue);
       }

    }  
}

