using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Random = System.Random;

namespace SunnyTown
{
    public class SpawnHandler : MonoBehaviour
    {
        private float instantiationTimer = 3f;
        public GameObject[] buildings;
        public bool buildSomeThing;
        private Dictionary<GameObject, bool> visibilityMap = new Dictionary<GameObject, bool>();


        // Start is called before the first frame update
        void Start()
        {

            foreach (GameObject building in buildings)
            {
                visibilityMap.Add(building, false);
                building.SetActive(false);
            }

        }
        
        public void PlayAnimation(string buildingName, float animationTime)
        {
            instantiationTimer = animationTime;
            try
            {
                Build((Building)Enum.Parse(typeof(Building), buildingName));
            }
            catch (ArgumentException e)
            {
                Debug.Log("Building animation was not found");
            }
            // if no building name is found it will simply play the progress bar only
        }
        
        private void Update()
        {
            if (buildSomeThing)
            {
                Random random = new Random();  
                buildings[random.Next(0, 8)].SetActive(true);
                buildSomeThing = false;
            }
        }

        // Method called to set building to appear
        private void Build(Building buildingName)
        {
            foreach (var building in visibilityMap.Keys)
            {
                // Setting visibility for the building to true
                if (building.name.Contains(buildingName.ToString()))
                {
                    building.SetActive(true);
                }

            }
        }
    }
}
