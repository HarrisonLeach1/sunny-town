using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class AcidRainer : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem rain;

        void Start()
        {
            rain.Play();
        }

        public void PlayAnim()
        {
            rain.Stop();
            Debug.Log("start plaing rain");
        }
    }
}