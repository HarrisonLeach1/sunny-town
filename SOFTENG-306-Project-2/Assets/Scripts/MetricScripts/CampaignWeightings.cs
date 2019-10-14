using UnityEngine;

namespace SunnyTown
{
    public struct CampaignWeightings
    {
        public float Happiness { get; private set; }
        public float Gold { get; private set; }
        public float EnvHealth { get; private set; }

        public CampaignWeightings(float happiness, float gold, float envHealth)
        {
            Debug.Log("Creating weightings, hap: " + happiness + " gold: " + gold + " envHealth: " + envHealth);
            Happiness = happiness;
            Gold = gold;
            EnvHealth = envHealth;
        }
    }
}