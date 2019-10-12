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
        public AudioClip loseScreenClip;


        void Start()
        {
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

        void PlayLoseScreenMusic()
        {
            _AudioSource.clip = loseScreenClip;
            _AudioSource.Play();
        }
    }
}