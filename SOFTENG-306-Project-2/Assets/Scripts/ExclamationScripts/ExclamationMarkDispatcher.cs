using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        float randomTime = (float)Random.Range(0.5f, 1.5f);
        //dont show exclamation mark while card showing 
        while (manager.GetCardStatus() || PopupButtonControllerScript.popupShowing){
            yield return new WaitForSeconds(1);
        }
        Debug.Log("creating card");
        PopupButtonControllerScript.CreatePopupButton(transform);
        StartCoroutine("CreateMinorCard", randomTime);
 
    }
    
}
