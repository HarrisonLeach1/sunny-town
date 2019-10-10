using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class ExclamationMarkDispatcher : MonoBehaviour
    {

        public GameObject popupManager;
        private CardManager manager;

        void Start()
        {
            PopupButtonControllerScript.Initialise();
            popupManager = GameObject.Find("CardManager");
            manager = popupManager.GetComponent<CardManager>();
            StartCoroutine("CreateMinorCard");
        }



        IEnumerator CreateMinorCard()
        {
            while (manager.GetCardStatus() || PopupButtonControllerScript.popupShowing)
            {
                yield return new WaitForSeconds(1);
            }
            float randomTime = (float) Random.Range(2f, 4f);
            //dont show exclamation mark while card showing 
            Debug.Log("creating card " + randomTime);
            yield return new WaitForSeconds(randomTime);
            PopupButtonControllerScript.CreatePopupButton(transform);
            StartCoroutine("CreateMinorCard", randomTime);

        }

    }
}