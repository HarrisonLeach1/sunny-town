using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class ExclamationDispatcher : MonoBehaviour
    {
        public GameObject manager;
        private bool isShowing;
        private CardManager cardManager;

        public MeshRenderer render;

        public Animator animator;
        void Start()
        {
            // GetComponent<MeshRenderer>().enabled = false;
            manager = GameObject.Find("CardManager");

            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = false;

            animator = GetComponent<Animator>();
            animator.SetBool("isShow",false);
            isShowing = false;
            cardManager = manager.GetComponent<CardManager>();  
            Debug.Log(GetComponent<MeshRenderer>().enabled);
            StartCoroutine("CreateExclamationMark");
        }
 
        IEnumerator CreateExclamationMark()
        {
            while (cardManager.GetCardStatus() || isShowing)
            {
                Debug.Log("still runnign");
                yield return new WaitForSeconds(1);
            }
            float randomTime = (float) Random.Range(2f, 4f);
            //dont show exclamation mark while card showing 
            Debug.Log("creating mark " + randomTime);
            yield return new WaitForSeconds(randomTime);
            //spawn card exclamation mark half the time
            if (randomTime >= 3f){
                foreach (Renderer r in GetComponentsInChildren<Renderer>())
                    r.enabled = true;
                isShowing = true;
                animator.SetBool("isShow",true);
                Debug.Log(randomTime);
            }
            StartCoroutine("CreateExclamationMark", randomTime);

        }
        void OnMouseDown()
        {
            if (isShowing && !cardManager.GetCardStatus()){
                animator.SetBool("isShow",false);
                isShowing = false;
                cardManager.DisplayMinorCard();
                foreach (Renderer r in GetComponentsInChildren<Renderer>())
                    r.enabled = false;
            } else {
                return;
            }
           
        }

        void changeShowState()
        {
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = false;
            animator.SetBool("isShow",false);
            isShowing = false;
        }
    }
}
