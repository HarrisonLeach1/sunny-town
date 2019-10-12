using UnityEngine;
using System.IO;

namespace SunnyTown
{
    public class SFXAudioManager : MonoBehaviour
    {
        public static MetricManager Instance { get; private set; }

        public AudioSource source;
        public AudioClip clip;

        private SFXAudioManager()
        {
            
        }

        public void PlayClickSound()
        {
            source.PlayOneShot(clip);
        }
    }
}