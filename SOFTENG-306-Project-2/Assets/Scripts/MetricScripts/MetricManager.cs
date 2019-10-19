using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SunnyTown
{
    /// <summary>
    /// The MetricManager is a singleton class that represents the 3 main metrics used in the game:
    /// PopulationHappiness, Gold and EnvironmentHealth. It is also responsible for rendering metrics
    /// bars and calculating the final score.
    /// </summary>
    public class MetricManager : MonoBehaviour
    {
        public static MetricManager Instance { get; private set; }

        [SerializeField] private GameObject metricsView;

        public int PopHappiness { get; private set; }
        public int Gold { get; private set; }
        public int EnvHealth { get; private set; }
        public CampaignWeightings campaignWeightings {get; set;}

        public int PrevPopHappiness { get; set; }
        public int PrevGold { get; set; }
        public int PrevEnvHealth { get; set; }

        private const int START_VALUE = 50;
        private const int MAX_VALUE = 100;
        private const int MIN_VALUE = 0;

        private WeatherController weatherController;


        private MetricManager()
        {
            this.PopHappiness = START_VALUE;
            this.Gold = START_VALUE;
            this.EnvHealth = START_VALUE;
            this.PrevGold = START_VALUE;
            this.PrevEnvHealth = START_VALUE;
            this.PrevPopHappiness = START_VALUE;
            campaignWeightings = new CampaignWeightings(0, 0, 0);
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

        private void Start()
        {
            RenderMetrics();
        }

        public void RenderMetrics()
        {
            var money = metricsView.transform.GetChild(0).GetComponent<Slider>();
            var happiness = metricsView.transform.GetChild(1).GetComponent<Slider>();
            var environment = metricsView.transform.GetChild(2).GetComponent<Slider>();

            StartCoroutine(AnimateMetric(money, PrevGold, Gold));
            StartCoroutine(AnimateMetric(happiness, PrevPopHappiness, PopHappiness));
            StartCoroutine(AnimateMetric(environment, PrevEnvHealth, EnvHealth));

            Debug.Log("Rendering Metrics: pop: " + PopHappiness + " gold: " + Gold + " envHealth: " + EnvHealth);

            PrevGold = Gold;
            PrevEnvHealth = EnvHealth;
            PrevPopHappiness = PopHappiness;
        }

        public int GetScore()
        {
            if (CardManager.Instance.GameWon)
            {
                return (int) (0.5 * EnvHealth + 0.25 * Gold + 0.25 * PopHappiness) + 100;
            }
            else
            {
                return (int) (0.5 * EnvHealth + 0.25 * Gold + 0.25 * PopHappiness);
            }
        }

        /// <summary>
        /// This method checks which end game is displayed when the game is won
        /// If any of the scores are less than 35, then display the bad ending
        /// </summary>
        /// <returns></returns>
        public bool ScoreLow()
        {
            //TODO: change numbers for playtesting
            if (EnvHealth < 45 || Gold < 45 || PopHappiness < 45)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        IEnumerator AnimateMetric(Slider metricBar, int oldValue, int newValue)
        {
            Image metricBarFill = metricBar.transform.GetChild(2).GetChild(0).GetComponent<Image>();

            int tempValue = oldValue;
            if (tempValue == newValue)
            {
                metricBarFill.color = new Color32(75, 75, 75, 255);
            }

            while (tempValue != newValue)
            {
                yield return null;
                if (tempValue < newValue)
                {
                    tempValue++;
                    metricBarFill.color = Color.green;
                }
                else if (tempValue > newValue)
                {
                    tempValue--;
                    metricBarFill.color = Color.red;
                }

                metricBar.value = (float) tempValue / MAX_VALUE;
            }

            metricBar.value = (float) newValue / MAX_VALUE;
        }

        public void UpdatePopHappiness(int value)
        {
            this.PopHappiness += value;
            this.PopHappiness += (int) Math.Round(value * campaignWeightings.Happiness);
            if (this.PopHappiness > MAX_VALUE)
            {
                this.PopHappiness = MAX_VALUE;
            }

            if (this.PopHappiness < MIN_VALUE)
            {
                this.PopHappiness = MIN_VALUE;
                SimpleDialogue endGameDialogue = new SimpleDialogue(new string[2]
                {
                    "Oh no, the people in our town became " +
                    "very upset with your decisions. They have voted you out of power.",
                    " Try keeping them happier next time. "
                }, "Advisory Board");
                CardManager.Instance.QueueGameLost(endGameDialogue);
            }
        }

        public void UpdateGold(int value)
        {
            this.Gold += value;
            this.PopHappiness += (int)Math.Round(value * campaignWeightings.Gold);

            if (this.Gold > MAX_VALUE)
            {
                this.Gold = MAX_VALUE;
            }

            if (this.Gold < MIN_VALUE)
            {
                this.Gold = MIN_VALUE;

                SimpleDialogue endGameDialogue = new SimpleDialogue(
                    new string[2]
                    {
                        "You have lost all your town's money. Now you cannot build the town.",
                        "Be careful when making decisions that involve spending money next time."
                    }, "Advisory Board");
                CardManager.Instance.QueueGameLost(endGameDialogue);
            }
        }

        public void UpdateEnvHealth(int value)
        {
            this.EnvHealth += value;
            this.PopHappiness += (int)Math.Round(value * campaignWeightings.EnvHealth);

            if (this.EnvHealth > MAX_VALUE)
            {
                this.EnvHealth = MAX_VALUE;
            }

            if (this.EnvHealth < MIN_VALUE)
            {
                this.EnvHealth = MIN_VALUE;

                SimpleDialogue endGameDialogue = new SimpleDialogue(new string[2]
                {
                    "Your decisions have led to a lot of damage to the environment." +
                    "Natural disasters have ravaged the city.",
                    " Take better care of your environment next time"
                }, "Advisory Board");


                CardManager.Instance.QueueGameLost(endGameDialogue);
            } else 
            {
                //notify weather controller of new metric  
                weatherController = WeatherController.Instance;
                weatherController.UpdateWeatherProbabilities((float) this.EnvHealth);
            }
        }
    }
}