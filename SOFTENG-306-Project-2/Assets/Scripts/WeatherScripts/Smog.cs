using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class Smog : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem smog;

        void Start()
        {
            smog.Stop();
        }

        public void PlayAnim()
        {
            smog.Stop();
            Debug.Log("start plaing smog");
        }
        public void StopAnim()
        {
            
        }
    }
}
