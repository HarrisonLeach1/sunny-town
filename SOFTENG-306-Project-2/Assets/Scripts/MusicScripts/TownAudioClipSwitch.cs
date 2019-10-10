using UnityEngine;
using System.IO;

namespace SunnyTown
{
    public class TownAudioClipSwitch : MonoBehaviour
    {
        public AudioSource _AudioSource;

        public AudioClip levelOneTownClip;
        public AudioClip levelTwoTownClip;
        public AudioClip levelThreeTownClip;


        void Start()
        {
            //_AudioSource = GetComponent<AudioSource>();
            //levelOneTownClip = GetComponent<AudioClip>();
            
            //levelOneTownClip = Resources.Load(Directory.GetCurrentDirectory() + "/Assets/Music/level_one_town_theme.mp3", typeof(AudioClip)) as AudioClip;
            //levelTwoTownClip = Resources.Load(Directory.GetCurrentDirectory() + "/Assets/Music/level_two_town_theme.mp3", typeof(AudioClip)) as AudioClip;
            //levelThreeTownClip = Resources.Load(Directory.GetCurrentDirectory() + "/Assets/Music/level_three_town_theme.mp3", typeof(AudioClip)) as AudioClip;
            
            _AudioSource.clip = levelOneTownClip;
            _AudioSource.Play();
        }

        void UpdateLevelOneClip()
        {
            _AudioSource.clip = levelOneTownClip;
            _AudioSource.Play();
        }

        void UpdateLevelTwoClip()
        {
            _AudioSource.clip = levelTwoTownClip;
            _AudioSource.Play();
        }

        void UpdateLevelThreeClip()
        {
            _AudioSource.clip = levelThreeTownClip;
            _AudioSource.Play();
        }
    }
}