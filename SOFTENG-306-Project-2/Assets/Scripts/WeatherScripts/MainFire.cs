using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SunnyTown
{
    public class MainFire : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem fire;

        void Start()
        {
            fire.Play();
        }

        public void PlayAnim()
        {
            fire.Stop();
            Debug.Log("start plaing fire");
        }

    }
}