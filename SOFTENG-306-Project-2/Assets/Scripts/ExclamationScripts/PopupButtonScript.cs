using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
    public class PopupButtonScript : MonoBehaviour
    {
        //have reference to popup button on start
        public GameObject popup;
        private Animator animator;

        public GameObject managerPopup;
        private CardManager manager;

        private Button button;

        private AnimationEvent animatorEvent;

        // Start is called before the first frame update
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
            //button.onClick.AddListener(() => manager.DisplayMinorCard(button));



        }


        

    }
}
