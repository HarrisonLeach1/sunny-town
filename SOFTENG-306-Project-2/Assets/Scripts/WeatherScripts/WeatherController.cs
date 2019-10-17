using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class WeatherController : MonoBehaviour
    {
        public static WeatherController Instance { get; private set; }

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

        private float envHealth;

        private MetricManager metricManager;

        private WeatherController()
        {
            this.probability = 0f;
            this.envHealth = 0f;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            metricManager = MetricManager.Instance;
            envHealth = (float) metricManager.EnvHealth;
            events = new WeatherEvent[4];
            events[0] = (WeatherEvent) rain;
            events[1] = (WeatherEvent) wildfire;
            events[2] = (WeatherEvent) storm;
            events[3] = (WeatherEvent) smog;

            //y = ((5/((x+15)/100))-4)/30 function to map score to probability
            Debug.Log("heres is prob");
            Debug.Log(probability);
        }        

        /* Function is called whenever environment health is changed, it updates the probability of 
         * of a weather event occurring 
         */
        public void UpdateWeatherProbabilities(float newHealth)
        {
            this.envHealth = newHealth;
            //recalculate probability of event happening
            if (this.envHealth >= 40){
                probability = 0;
            } else {
                probability = (((5/((this.envHealth+25)/100))-4)+2)/16;
            }
            Debug.Log("new probability is "+probability+" new health is "+this.envHealth);

        }




    }
}
