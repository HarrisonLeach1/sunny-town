using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyTown
{
    public class CheatsManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            //cheatcode of leveling up
            if (Input.GetKey(KeyCode.L))
            {
                Debug.Log("Level up cheatcode entered");
                //LevelControl.Instance.NextLevel();
            }
            
            //cheatcode for maxing out metrics
            if (Input.GetKey(KeyCode.M))
            {
                Debug.Log("Metrics boost cheatcode entered");
                
                MetricsModifier metricsModifier = new MetricsModifier(95, 95, 95);
                metricsModifier.Modify();
            }
        }
    }    
}

