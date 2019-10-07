using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    /** Class handles creation and placement of an exclamtion mark button but getting the camera, canvas and location that the button needs to be spawned at
     */
    public class PopupButtonControllerScript : MonoBehaviour
    {
        private static PopupButtonScript popupButton;
        private static GameObject canvas;

        public static bool popupShowing;

        /** When initialised class needs to know the canvas object of the game as well as the associated exclamation mark prefab that needs to be spawned 
         */
        public static void Initialise()
        {
            canvas = GameObject.Find("UI");
            popupButton = Resources.Load<PopupButtonScript>("Prefabs/PopupParent");
        }

        /** Method called in coroutine in the dispatcher class, it handles the creation and positioning of a new exclamation mark button by 
         ** converting its world position to screen position 
         */
        public static void CreatePopupButton(Transform location)
        {
            popupShowing = true;
            Debug.Log("create button");
            PopupButtonScript instance = Instantiate(popupButton);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
            //adjust positioning of exclamation mark so its not directly over centre of city hall
            Vector2 finalPosition = new Vector2(screenPosition.x + 70, screenPosition.y + 120);
            instance.transform.SetParent(canvas.transform, true);
            instance.transform.position = finalPosition;
            Debug.Log(instance.transform.position);
        }

        /** Method called when the exclamation mark fades either after user has clicked on it or after it has timed out
         ** it resets the boolean maintaining the state of the popup display
         */
        void DisableExclamationState()
        {
            popupShowing = false;
            Debug.Log("status is " + popupShowing);
        }

    }
}
