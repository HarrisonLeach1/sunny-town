using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
    /** Class is attached to the popup button UI element, it handles destroying the button UI after its animation has completed
     */
    public class PopupButtonScript : MonoBehaviour
    {
        public GameObject popup;
        private Animator animator;

        public GameObject managerPopup;
        private CardManager manager;

        private Button button;

        private AnimationEvent animatorEvent;

        /** Function adds a listener to the button click action which initiates the creation of a dialogue box 
         */
        void OnEnable()
        {
            popup = GameObject.Find("Popup");
            managerPopup = GameObject.Find("CardManager");
            manager = managerPopup.GetComponent<CardManager>();
            animator = popup.GetComponent<Animator>();
            animatorEvent = new AnimationEvent();
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            Destroy(gameObject, clipInfo[0].clip.length);
            button = animator.GetComponent<Button>();
            button.onClick.AddListener(() => manager.DisplayMinorCard(button));
        }

    }
}
