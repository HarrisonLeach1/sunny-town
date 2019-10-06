using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo: link up button press with card, using card manager,
// create the probability factor for the event 
// using displayminorcard() function 
public class PopupButtonControllerScript : MonoBehaviour
{
    private static PopupButtonScript popupButton; 
    private static GameObject canvas;

    public static bool popupShowing;
    public static void Initialise()
    {   
        canvas = GameObject.Find("UI");
        popupButton = Resources.Load<PopupButtonScript>("Prefabs/PopupParent");
    }
    public static void CreatePopupButton( Transform location)
    {
        popupShowing = true;
        Debug.Log("create button");
        PopupButtonScript instance = Instantiate(popupButton);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        Vector2 finalPosition = new Vector2(screenPosition.x/3, screenPosition.y/3);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = finalPosition;
        Debug.Log(instance.transform.position);
    }

    
    void DisableExclamationState()
    {
        popupShowing = false;
        Debug.Log("status is "+popupShowing);
    }

}
