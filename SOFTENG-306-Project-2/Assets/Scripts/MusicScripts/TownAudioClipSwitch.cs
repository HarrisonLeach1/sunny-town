using UnityEngine;
using System.IO;

namespace SunnyTown
{
    /// <summary>
    /// The TownAudioClipSwitch is responsible for playing music in the World Scene. It is responsible
    /// for playing different music for each of the game's 3 levels.
    /// </summary>
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

        public void UpdateLevelOneClip()
        {
            _AudioSource.clip = levelOneTownClip;
            _AudioSource.Play();
        }

        public void UpdateLevelTwoClip()
        {
            _AudioSource.clip = levelTwoTownClip;
            _AudioSource.Play();
        }

        public void UpdateLevelThreeClip()
        {
            _AudioSource.clip = levelThreeTownClip;
            _AudioSource.Play();
        }

        public void PlayLoseScreenMusic()
        {
            _AudioSource.clip = loseScreenClip;
            _AudioSource.Play();
        }
    }
}