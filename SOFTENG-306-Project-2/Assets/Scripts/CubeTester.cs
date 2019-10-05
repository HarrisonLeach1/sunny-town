using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTester : MonoBehaviour
{
      void Start()
    {
        PopupButtonControllerScript.Initialise();
    }
    void OnMouseDown()
    {
        Debug.Log(transform);
        PopupButtonControllerScript.CreatePopupButton(transform);
    }
}
