using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class AcidRain : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem rain;

        void Start()
        {
            rain.Stop();
        }

        public void PlayAnim()
        {
            rain.Play();
            Debug.Log("start plaing rain");
        }
        public void StopAnim()
        {
            rain.Stop();
        }
    }
}