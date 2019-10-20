using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace SunnyTown
{
    /// <summary>
    /// The VolumeController script is responsible for mapping the value of the volume slider that can be set in the Settings page
    /// to the volume of AudioSources it is added to
    /// </summary>
    public class VolumeController : MonoBehaviour
    {

        public VolumeController Instance { get; set; }
        public AudioMixer SFXMixer;
        public AudioMixer MusicMixer;
        public Slider SFXslider;
        public Slider MusicSlider;
        public float initialMusicValue;
        public float initialSFXValue;


        void Start()
        {
            SFXslider.value = PlayerPrefs.GetFloat("SFXVolume");
            Debug.Log("Getting sfx old val: " + SFXslider.value);
            MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            Debug.Log("Getting msuci old val: " + MusicSlider.value);
            SetLevel();
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetLevel()
        {
            float SFXsliderValue = SFXslider.value;
            float MusicSliderValue = MusicSlider.value;
            //using logarithmic conversion
            //takes 0.001 and 1 value into a value between -80 and 0 on a log scale
            MusicMixer.SetFloat("MusicVolume", Mathf.Log10(MusicSliderValue) * 20);
            SFXMixer.SetFloat("SFXVolume", Mathf.Log10(SFXsliderValue) * 20);

            PlayerPrefs.SetFloat("SFXVolume", SFXsliderValue);
            PlayerPrefs.SetFloat("MusicVolume", MusicSliderValue);
        }

    }
}

