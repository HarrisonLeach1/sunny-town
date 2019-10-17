using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class Hurricaner : WeatherEvent
    {
        [SerializeField]
        private ParticleSystem storm; 

        // [SerializeField]
        // private GameObject obj;

        private Animator animator;


        [SerializeField]
        private GameObject obj;

        private bool showing;

        void Start()
        {
            storm.Stop();
            obj = GameObject.Find("storm");
            animator = obj.GetComponent<Animator>();
            animator.SetBool("triggerHurricane",true);
        }

        public void PlayAnim()
        {
            Debug.Log("start plaing storm");
            storm.Play();
            animator = obj.GetComponent<Animator>();
            animator.SetBool("triggerHurricane",true);
            // if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hurricane")){
            //     Debug.Log("fuck off");
            // } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("InvisHurricane")) {
            //     Debug.Log("fuck righjt off");
            // } else {
            //     Debug.Log("no way");
            // }

        }

        void OnEndOfAnim()
        {
            Debug.Log("stopped");
            storm.Stop();
            animator = obj.GetComponent<Animator>();
            animator.SetBool("triggerHurricane", false);
        }
        
    }
}
