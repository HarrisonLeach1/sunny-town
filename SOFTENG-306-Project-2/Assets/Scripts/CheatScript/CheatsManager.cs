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
            //cheatcode of leveling up
            if (Input.GetKey(KeyCode.L))
            {
//                if (levelController.CurrentLevel == 1)
//                {
////                    var card
////                    levelController
//                }
                Debug.Log("Level up cheatcode entered");

                
            }
            
            //cheatcode for maxing out metrics
            if (Input.GetKey(KeyCode.M))
            {
                Debug.Log("Metrics boost cheatcode entered");
                new MetricsModifier(95, 95, 95).Modify();
                MetricManager.Instance.RenderMetrics();
            }
        }
    }    
}

