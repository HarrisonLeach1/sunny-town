using UnityEngine;
using System.IO;

namespace SunnyTown
{
    public class SFXAudioManager : MonoBehaviour
    {
        public static MetricManager Instance { get; private set; }

        public AudioSource source;
        public AudioClip buttonClip;
        public AudioClip constructionClip;
        public AudioClip notificationClip;

        private SFXAudioManager()
        {
            
        }

        public void PlayClickSound()
        {
            source.PlayOneShot(buttonClip);
        }

        public void PlayConstructionSound()
        {
            source.PlayOneShot(constructionClip);
        }

        public void PlayNotifcationSound()
        {
            source.PlayOneShot(notificationClip);
        }
        
        
    }
}