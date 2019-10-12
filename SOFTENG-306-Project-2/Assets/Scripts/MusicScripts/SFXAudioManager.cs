using UnityEngine;
using System.IO;

namespace SunnyTown
{
    public class SFXAudioManager : MonoBehaviour
    {
        public static SFXAudioManager Instance { get; private set; }

        public AudioSource source;
        public AudioClip buttonClip;
        public AudioClip constructionClip;
        public AudioClip notificationClip;

        private SFXAudioManager()
        {
            
        }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayClickSound()
        {
            source.PlayOneShot(buttonClip);
        }

        public void PlayConstructionSound()
        {
            source.PlayOneShot(constructionClip);
        }

        public void PlayNotificationSound()
        {
            source.PlayOneShot(notificationClip);
        }
        
        
    }
}