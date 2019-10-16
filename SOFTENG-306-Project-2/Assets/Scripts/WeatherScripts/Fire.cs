using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SunnyTown
{
    public class Fire : WeatherEvent
    {
        [SerializeField]
        private ParticleSystem fire;

        private bool status = false;

        [SerializeField]
        private Button button;

        void Start()
        {
            button.onClick.AddListener(OnClick); 
            fire.Stop();
        }

        /** Right now rain is triggered on button click by the test button, will need to implement a weather controller that fires weather event
        ** as a function of the environmental health 
        */    
        void OnClick()
        {
            if (!status){
                status = !status;
                fire.Play();

            } else {
                status = !status;
                fire.Stop();
            }
        }
    }
}
