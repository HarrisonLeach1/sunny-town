using UnityEngine;

namespace SunnyTown
{
    /// <summary>
    /// A CampaignWeightings object is a dto representing the multipliers to apply to metrics
    /// to represent the effects of a Campaign.
    /// </summary>
    public struct CampaignWeightings
    {
        public float Happiness { get; private set; }
        public float Gold { get; private set; }
        public float EnvHealth { get; private set; }

        public CampaignWeightings(float happiness, float gold, float envHealth)
        {
            Happiness = happiness;
            Gold = gold;
            EnvHealth = envHealth;
        }
    }
}