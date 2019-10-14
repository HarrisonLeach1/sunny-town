using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class ExclamationDispatcher : MonoBehaviour
    {
        public GameObject manager;
        private bool markSpawned;
        private CardManager cardManager;

        public MeshRenderer render;

        public Animator animator;
        void Start()
        {
            manager = GameObject.Find("CardManager");
            // dont render the individual shapes that make up the exclamation mark 
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = false;

            animator = GetComponent<Animator>();
            // set animator to default state 
            animator.SetBool("isShow",false);
            markSpawned = false;
            cardManager = manager.GetComponent<CardManager>();  
            StartCoroutine("CreateExclamationMark");
        }
    
        /** Method starts the exclamation mark spawning animation, and reschedules itself at a random time in the future
         */
        IEnumerator CreateExclamationMark()
        {
            // wait while a card or an exclamation mark is already showing 
            while (cardManager.GetCardStatus() || markSpawned)
            {
                yield return new WaitForSeconds(1);
            }
            float randomTime = (float) Random.Range(2f, 4f);
            //dont show exclamation mark while card showing 
            Debug.Log("creating mark " + randomTime);
            //spawn card exclamation mark half the time
            if (randomTime >= 3f){
                foreach (Renderer r in GetComponentsInChildren<Renderer>())
                    r.enabled = true;
                markSpawned = true;
                animator.SetBool("isShow",true);
                SFXAudioManager.Instance.PlayNotificationSound();
                Debug.Log(randomTime);
            } else {
                Debug.Log("waiting");
                yield return new WaitForSeconds(randomTime);
            }
            StartCoroutine("CreateExclamationMark", randomTime);

        } 

        /** Transition to default state (invisible) when it has been clicked  
         ** and create the minor cards to be displayed 
         */
        void OnMouseDown()
        {
            if (markSpawned && !cardManager.GetCardStatus()){
                animator.SetBool("isShow",false);
                markSpawned = false;
                cardManager.DisplayMinorCard();
                foreach (Renderer r in GetComponentsInChildren<Renderer>())
                    r.enabled = false;
            } else {
                return;
            }
           
        }

        /** At the end of the animation, set the animator to default invisible state
         */
        void OnEndOfAnimation()
        {
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = false;
            animator.SetBool("isShow",false);
            markSpawned = false;
        }
    }
}
