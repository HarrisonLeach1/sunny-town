using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class CheatsManager : MonoBehaviour
    {

        private LevelControl levelController;
        private LevelProgressScript levelProgressScript;
        
        // Start is called before the first frame update
        void Start()
        {
            levelProgressScript = GameObject.Find("LevelProgress").GetComponent<LevelProgressScript>();
            levelController = GameObject.Find("LevelManager").GetComponent<LevelControl>();
        }

        // Update is called once per frame
        void Update()
        {
            Reset();
            //cheatcode of leveling up
            if (Input.GetKey(KeyCode.L) && !levelUpPressed)
            {
                decay = 1f;
                levelUpPressed = true;

                Debug.Log("Level up cheatcode entered");
                if (levelController.CurrentLevel == 1)
                {
                    var card = CardManager.Instance.SetLevelTwo();
                    levelProgressScript.UpdateValue((PlotCard)card);
                }
                else if (levelController.CurrentLevel == 2)
                {
                    var card = CardManager.Instance.SetLevelThree();
                    levelProgressScript.UpdateValue((PlotCard)card);
                }
            }
            
            //cheatcode for maxing out metrics
            if (Input.GetKey(KeyCode.M))
            {
                Debug.Log("Metrics boost cheatcode entered");
                new MetricsModifier(95, 95, 95).Modify();
                MetricManager.Instance.RenderMetrics();
            }
        }

        private bool levelUpPressed = false;
        private float decay;

        private void Reset()
        {
            if (levelUpPressed && decay > 0)
            {
                decay -= Time.deltaTime;
            }
            if (decay < 0)
            {
                decay = 0;
                levelUpPressed = false;
            }
        }
    }    
}

