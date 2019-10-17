using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class WeatherController : MonoBehaviour
    {
        private WeatherEvent[] events;

        [SerializeField]
        private AcidRainer rain;

        [SerializeField]
        private MainFire wildfire;
        
        [SerializeField]
        private Hurricaner storm;

        [SerializeField]
        private Smogger smog; 

        private float probability;

        void Start()
        {
            
            float envHealth = 50;
            events = new WeatherEvent[4];
            events[0] = (WeatherEvent) rain;
            events[1] = (WeatherEvent) wildfire;
            events[2] = (WeatherEvent) storm;
            events[3] = (WeatherEvent) smog;
            
            storm.PlayAnim();

            //y = ((5/((x+15)/100))-4)/30 function to map score to probability
            if (envHealth >= 40){
                probability = 0;
            } else {
                probability = (((5/((envHealth+25)/100))-4)+2)/16;
            }
            Debug.Log("heres is prob");
            Debug.Log(probability);
        }        

        /* Function is called whenever environment health is changed, it updates the probability of 
         * of a weather event occurring 
         */
        void UpdateWeatherProbabilities()
        {
            




        }




    }
}
