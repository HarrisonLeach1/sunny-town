using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class ExclamationDispatcher : MonoBehaviour
    {
        public static ExclamationDispatcher Instance { get; private set; }
        public GameObject manager;
        private bool markSpawned;
        private CardManager cardManager;

        public int clickCount; 
        public MeshRenderer render;

        public Animator animator;
        void Start()
        {
            Debug.Log("Started dispatcher");
            manager = GameObject.Find("CardManager");
            // dont render the individual shapes that make up the exclamation mark 
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = false;
            clickCount = 0;
            animator = GetComponent<Animator>();
            // set animator to default state 
            animator.SetBool("isShow", false);
            markSpawned = false;
            cardManager = manager.GetComponent<CardManager>();
            StartCoroutine("CreateExclamationMark");
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
        
        
         /// <summary>
         ///  Starts the exclmation mark spawining animation, and reschedules itself at a random time in the future
         /// </summary>
        IEnumerator CreateExclamationMark()
        {
            // wait while a card or an exclamation mark is already showing 
            while (cardManager.CurrentGameState != CardManager.GameState.WaitingForEvents || markSpawned)
            {
                Debug.Log("ex waiting");
                yield return new WaitForSeconds(1);
            }
            float randomTime = (float)Random.Range(2f, 4f);
            //dont show exclamation mark while card showing 
            Debug.Log("creating mark " + randomTime);
            //spawn card exclamation mark half the time
            if (randomTime >= 3f)
            {
                foreach (Renderer r in GetComponentsInChildren<Renderer>())
                    r.enabled = true;
                markSpawned = true;
                animator.SetBool("isShow", true);
                SFXAudioManager.Instance.PlayNotificationSound();
                Debug.Log(randomTime);
            }
            else
            {
                Debug.Log("waiting");
                yield return new WaitForSeconds(randomTime);
            }
            StartCoroutine("CreateExclamationMark", randomTime);

        }

         /// <summary>
         /// Transition to default state (invisible) when mark has been clicked
         /// also queues the minor cards to be displayed
         /// </summary>
        void OnMouseDown()
        {
            if (markSpawned && cardManager.CurrentGameState == CardManager.GameState.WaitingForEvents)
            {
                animator.SetBool("isShow", false);
                clickCount++;
                markSpawned = false;
                cardManager.QueueMinorCard();
                foreach (Renderer r in GetComponentsInChildren<Renderer>())
                    r.enabled = false;
            }
            else
            {
                return;
            }

        }

         /// <summary>
         /// At the end of animation, set the animator to default invisible state
         /// </summary>
        void OnEndOfAnimation()
        {
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = false;
            animator.SetBool("isShow", false);
            markSpawned = false;
        }
    }
}
