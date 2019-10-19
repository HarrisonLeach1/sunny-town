using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    /// <summary> 
    /// Singleton controller class that manages the execution of random weather events
    /// Weather events are triggered based on a probability calculated from the environment health metric
    /// </summary>
    public class WeatherController : MonoBehaviour
    {

        public enum ClimateEvent 
        {
            NoEvent,
            AcidRain,
            WildFire,
            Hurricane,
            Smog
        }

        public ClimateEvent currentEvent;
        public static WeatherController Instance { get; private set; }

        [SerializeField]
        private AcidRain rain;

        [SerializeField]
        private Fire wildfire;

        [SerializeField]
        private Hurricane storm;

        [SerializeField]
        private Smog smog; 

        public float probability;

        private float envHealth;

        private int turnCounter;

        private int eventIndex;

        private const int TURN_COUNTER = 4;
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
            turnCounter = TURN_COUNTER;
            currentEvent = ClimateEvent.NoEvent;

            eventIndex = Random.Range(0,4);

            //y = ((5/((x+15)/100))-4)/30 function to map score to probability          
        }        

        /// <summary> 
        ///Function is called whenever environment health is changed, it updates the probability of a weather event occurring 
        /// </summary>
        /// <param name="newHealth"> new envrionment health value </param>
        public void UpdateWeatherProbabilities(float newHealth)
        {
            this.envHealth = newHealth;
            //recalculate probability of event happening
            if (this.envHealth >= 40){
                probability = 0;
            } else {
                //TODO: balance functions
                probability = (((5/((this.envHealth+25)/100))-4)+2)/16;
            }
            Debug.Log("new probability is "+probability+" new health is "+this.envHealth);
        }

        /// <summary>
        /// Function is called Whenever game state changes in the CardManager class
        /// it will attempt to trigger weather event if in the right state and turn
        /// </summary>
        public void CheckGameStatus()
        {
            CardManager.GameState state = cardManager.CurrentGameState;
            //if waiting for event then free to fire weather event   
            if (state.Equals(CardManager.GameState.WaitingForEvents))
            {
                turnCounter = (turnCounter == 1)? 1 : --turnCounter;
                if (turnCounter == 1)
                {
                    this.AttemptWeatherEvent();
                }
            }
        }

        /// <summary>
        /// Function tries to trigger a weather event given the current probability by generating a random 
        /// number and seeing if its less than or equal to the probability
        /// </summary>
        private void AttemptWeatherEvent()
        {
            float randomTime = (float)Random.Range(0f, 1f);
            Debug.Log("Random number is "+randomTime+" prob is "+probability);
            if (randomTime <= this.probability)
            {
                StartCoroutine("TriggerWeatherEvent");
            }
        }

        /// <summary>
        /// Coroutine that handles execution of weather event, it finds the current weather event to execute
        /// and makes a call to display the weather card
        /// </summary>
        IEnumerator TriggerWeatherEvent() 
        {
            // eventindex is randomised at start, order of weather events is cyclic
            //reset turn counter
            turnCounter = TURN_COUNTER;
            Debug.Log("triggering weather "+eventIndex);
            if (weatherEvents.TryGetValue(eventIndex, out ClimateEvent value))
            {
                currentEvent = value;
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
                cardManager.SetState(CardManager.GameState.WeatherEvent);
            }
            yield return null;
        }

        /// <summary>
        /// Callback function executed when player clicks continue on a weather card, it handles
        /// incrementing the current weather event and stopping the animation of the weather event
        /// </summary>
        public void StopAnim()
        {
            Debug.Log(eventIndex);
            if (weatherEvents.TryGetValue(eventIndex, out ClimateEvent value))
            {
                switch(value)
                {
                    case ClimateEvent.AcidRain:
                    rain.StopAnim();
                    Debug.Log("cut raining");
                    break;

                    case ClimateEvent.Hurricane:
                    storm.StopAnim();
                    Debug.Log("cut storming");
                    break;

                    case ClimateEvent.WildFire:
                    wildfire.StopAnim();
                    Debug.Log("cut firing");
                    break;

                    case ClimateEvent.Smog:
                    smog.StopAnim();
                    Debug.Log("cut smogging");
                    break;
                }
            }
            eventIndex = (eventIndex==3) ? 0 : ++eventIndex;
        }
    }
}
