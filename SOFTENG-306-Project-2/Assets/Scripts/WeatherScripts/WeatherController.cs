using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SunnyTown
{
    /// <summary> 
    /// Singleton controller class that manages the execution of random weather events
    /// Weather events are triggered based on a probability calculated from the environment health metric
    /// <summary>
    public class WeatherController : MonoBehaviour
    {

        private enum ClimateEvent 
        {
            AcidRain,
            WildFire,
            Hurricane,
            Smog
        }
        public static WeatherController Instance { get; private set; }

        //private WeatherEvent[] events;

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

        private CardManager cardManager;

        private Dictionary<int, ClimateEvent> weatherEvents;
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
            cardManager = CardManager.Instance;
            envHealth = (float) metricManager.EnvHealth;
            weatherEvents = new Dictionary<int, ClimateEvent>();
            weatherEvents.Add(0,ClimateEvent.AcidRain);
            weatherEvents.Add(1,ClimateEvent.WildFire);
            weatherEvents.Add(2,ClimateEvent.Hurricane);
            weatherEvents.Add(3,ClimateEvent.Smog);

            //events = new WeatherEvent[4];
            // events[0] = (WeatherEvent) rain;
            // events[1] = (WeatherEvent) wildfire;
            // events[2] = (WeatherEvent) storm;
            // events[3] = (WeatherEvent) smog;

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

        public void CheckGameStatus()
        {
            CardManager.GameState state = cardManager.CurrentGameState;
            Debug.Log("chjecking the state "+state.ToString());
            //if waiting for event then free to fire weather event   
            if (state.Equals(CardManager.GameState.WaitingForEvents))
            {
                this.AttemptWeatherEvent();
            }
        }

        private void AttemptWeatherEvent()
        {
            float randomTime = (float)UnityEngine.Random.Range(0f, 1f);
            if (randomTime <= this.probability)
            {
                Debug.Log("attempgint weather");
                //if random number is lower than probability then fire event
                StartCoroutine(TriggerWeatherEvent(ChangeState));
            }
        }

        IEnumerator TriggerWeatherEvent(Action callback) 
        {
            cardManager.SetState(CardManager.GameState.WaitingForFeedback);
            int eventIndex = UnityEngine.Random.Range(0,4);
            Debug.Log("triggering weather");
            if (weatherEvents.TryGetValue(eventIndex, out ClimateEvent value))
            {
                switch(value)
                {
                    case ClimateEvent.AcidRain:
                    rain.PlayAnim();
                    Debug.Log("raining");
                    break;

                    case ClimateEvent.Hurricane:
                    storm.PlayAnim();
                    Debug.Log("storming");
                    break;

                    case ClimateEvent.WildFire:
                    wildfire.PlayAnim();
                    Debug.Log("firing");
                    break;

                    case ClimateEvent.Smog:
                    smog.PlayAnim();
                    Debug.Log("smogging");
                    break;
                }
            }
            yield return null;
            if (callback != null) callback();
        }
        
        private void ChangeState()
        {
            Debug.Log("end of anim here");
            cardManager.SetState(CardManager.GameState.WaitingForEvents);
        }
    }
}
