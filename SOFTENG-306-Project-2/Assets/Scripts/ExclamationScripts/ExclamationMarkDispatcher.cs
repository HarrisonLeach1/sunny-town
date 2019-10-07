using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class ExclamationMarkDispatcher : MonoBehaviour
    {

        public GameObject popupManager;
        private CardManager manager;

        /** On start up the class needs to find the singleton instance of CardManager and start the coroutine that 
         ** periodically spawns an exclamation mark event  
         */
        void Start()
        {
            PopupButtonControllerScript.Initialise();
            popupManager = GameObject.Find("CardManager");
            manager = popupManager.GetComponent<CardManager>();
            StartCoroutine("CreateMinorCard");
        }


        /** Coroutine that calls the CreatePopupButton() function after a random set time whenever a card is not display or an exclamation mark is not showing
         */
        IEnumerator CreateMinorCard()
        {
            while (manager.GetCardStatus() || PopupButtonControllerScript.popupShowing)
            {
                yield return new WaitForSeconds(1);
            }

            Debug.Log("in here");
            float randomTime = (float) Random.Range(2f, 4f);
            //dont show exclamation mark while card showing 
            Debug.Log("creating card " + randomTime);
            yield return new WaitForSeconds(randomTime);
            PopupButtonControllerScript.CreatePopupButton(transform);
            StartCoroutine("CreateMinorCard", randomTime);

        }

    }
}