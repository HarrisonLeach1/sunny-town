using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupButtonControllerScript : MonoBehaviour
{
    private static PopupButtonScript popupButton; 
    private static GameObject canvas;
    public static void Initialise()
    {   
        canvas = GameObject.Find("Canvas");
        //popupButton = Resources.Load("Prefabs/PopupParent") as GameObject;
        popupButton = Resources.Load<PopupButtonScript>("Prefabs/PopupParent");
    }
    public static void CreatePopupButton( Transform location)
    {
        Debug.Log("create button");
        PopupButtonScript instance = Instantiate(popupButton);
        if (instance == null){
            Debug.Log("its null");
        } else {
            Debug.Log("not nul");
        }
        //Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        //instance.transform.position = screenPosition;
    }

}
