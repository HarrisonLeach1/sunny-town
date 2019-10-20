using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace SunnyTown
{
    public class LevelControl : MonoBehaviour
    {
        public GameObject[] levels;
        public GameObject[] levelUpTransition;
        public bool levelUp;
        public float cloudFadeDuration = 1.0f; 

        private int currentLevel;
        private Dictionary<int, int> metricMultiplierDictionary;
        public int Multiplier { get; set; }

        private TownAudioClipSwitch townAudioClipSwitch;

        // Start is called before the first frame update
        void Start()
        {
            foreach (GameObject level in levels)
            {
                // Used for setting cloud animation to false at the beginning 
                foreach (GameObject cloud in levelUpTransition)
                {
                    cloud.GetComponent<ParticleSystem>().enableEmission = false;
                }
                // Making level 1 active when the game starts
                if (level.name.Contains("1"))
                {
                    print(level.name);
                    currentLevel = 1;
                    level.SetActive(true);
                }
                else
                {
                    print("This happens too");
                    level.SetActive(false);
                }
                
                
                townAudioClipSwitch = GameObject.Find("TownAudioClipSwitch").GetComponent<TownAudioClipSwitch>();

            }

            metricMultiplierDictionary = new Dictionary<int, int>();
            metricMultiplierDictionary.Add(1, 1);
            metricMultiplierDictionary.Add(2, 2);
            metricMultiplierDictionary.Add(3, 3);
            Multiplier = 1;
        }

        private void Update()
        {
            if (levelUp)
            {
                NextLevel();
                levelUp = false;
            }
        }

        /// <summary>
        ///  This method can be called to advance to the next level
        /// </summary>
        public void NextLevel()
        {
            // Checking if the current level is below 3
            if (currentLevel < 3)
                Debug.Log("current level: " +currentLevel);
            {
                // Setting the next level to be active
                currentLevel++;
                
                //update music
                progressMusic();
                
                // update multiplier 
                if (metricMultiplierDictionary.TryGetValue(currentLevel, out int value))
                {
                    Multiplier = value;
                }
                
                foreach (GameObject level in levels)
                {
                    
                    if (level.name.Contains(currentLevel.ToString()))
                    {
                        // Starting cloud animation for transition 
                        foreach (GameObject cloud in levelUpTransition)
                        {
                            cloud.GetComponent<ParticleSystem>().enableEmission = true;
                        }
                        
                        // Waiting before the next level is loaded
                        StartCoroutine(LateCall());

                    }
                    
                }
            }

        }

        IEnumerator LateCall()
        {
            yield return new WaitForSeconds(3);

            if (currentLevel < 5)
            {
                Debug.Log("setting active level: " + currentLevel);
                levels[currentLevel - 1].SetActive(true);
                //Making previous level inactive
                levels[currentLevel - 2].SetActive(false);
            }

            // Fading out the particles
            foreach (GameObject cloud in levelUpTransition)
            {
                cloud.GetComponent<ParticleSystem>().enableEmission = false;
            }
            
        }

        private void progressMusic()
        {
            if (currentLevel == 1)
            {
                townAudioClipSwitch.UpdateLevelOneClip();
                return;
            }

            if (currentLevel == 2)
            {
                townAudioClipSwitch.UpdateLevelTwoClip();
                return;
            }

            townAudioClipSwitch.UpdateLevelThreeClip();
        }
    }

}
