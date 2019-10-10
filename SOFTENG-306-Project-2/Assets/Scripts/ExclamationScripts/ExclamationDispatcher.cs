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
            isShowing = true;
            animator.SetBool("isShow",true);
            StartCoroutine("CreateExclamationMark", randomTime);

        }
        void OnMouseDown()
        {
            if (isShowing && !cardManager.GetCardStatus()){
                animator.SetBool("isShow",false);
                isShowing = false;
                cardManager.DisplayMinorCard();
            } else {
                return;
            }
           
        }

        // void OnEnable()
        // {
        //     Debug.Log("starting routine");
        //     isShowing = false;
        //     animator.SetBool("isShow",false);
        //     StartCoroutine("CreateExclamationMark");
        // }

        void changeShowState()
        {
            animator.SetBool("isShow",false);
            isShowing = false;
        }
    }
}
