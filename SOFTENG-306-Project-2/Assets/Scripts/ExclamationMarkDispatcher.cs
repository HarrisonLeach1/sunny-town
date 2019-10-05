using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationMarkDispatcher : MonoBehaviour
{
      void Start()
    {
        PopupButtonControllerScript.Initialise();
    }
    void OnMouseDown()
    {
        PopupButtonControllerScript.CreatePopupButton(transform);
        Debug.Log("pos is " + transform.position);
    }
}
