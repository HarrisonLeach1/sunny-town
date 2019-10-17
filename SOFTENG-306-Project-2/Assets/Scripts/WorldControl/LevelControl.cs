using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public GameObject[] levels;
    public GameObject[] levelUpTransition;

    private int currentLevel;
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
        }
    }

    /// <summary>
    ///  This method can be called to advance to the next level
    /// </summary>
    public void NextLevel()
    {
        // Checking if the current level is below 3
        if (currentLevel < 3)
        {
            // Setting the next level to be active
            currentLevel++;
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

    /// <summary>
    /// Method is called to finish animation transition to the next level
    /// </summary>
    /// <returns></returns>
    IEnumerator LateCall()
    {
        // Waits 3 seconds for the clouds before activating the new level
        yield return new WaitForSeconds(3);
        levels[currentLevel - 1].SetActive(true);
        //Making previous level inactive
        levels[currentLevel-2].SetActive(false);
        
        // Fading out the particles
        foreach (GameObject cloud in levelUpTransition)
        {
            cloud.GetComponent<ParticleSystem>().enableEmission = false;
        }
                    

    }
}
