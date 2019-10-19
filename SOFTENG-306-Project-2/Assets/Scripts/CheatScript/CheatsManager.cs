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
            if (Input.GetKey(KeyCode.L))
            {
                Debug.Log("Level up cheatcode entered");
                LevelControl.Instance.NextLevel();
            }
            
            //cheatcode for maxing out metrics
        }
    }    
}

