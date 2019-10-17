using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SunnyTown
{
    public class Fire : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem fire;

        void Start()
        {
            fire.Stop();
        }

        public void PlayAnim()
        {
            fire.Stop();
            Debug.Log("start plaing fire");
        }

    }
}