using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

namespace SunnyTown
{
    public class SpawnHandler : MonoBehaviour
    {
        public GameObject[] buildings;
        private Dictionary<GameObject, bool> visibilityMap = new Dictionary<GameObject, bool>();
        private GameObject buildingCloud;
        private float instantiationTimer = 3f;

        // Start is called before the first frame update
        void Start()
        {

            foreach (GameObject building in buildings)
            {
                visibilityMap.Add(building, false);
            }

        }

        // Update is called once per frame
        void Update()
        {
            StopBuildingCloud();
        }

        public void PlayAnimation(string buildingName, float animationTime)
        {
            instantiationTimer = animationTime;
            if (buildingName.Equals(Building.CoalMine.ToString()))
            {
                Debug.Log("Building coal mine");
                Build(Building.CoalMine);
            }
            else if (buildingName.Equals(Building.PowerPlant.ToString()))
            {
                Debug.Log("Building powerplant");
                Build(Building.PowerPlant);
            }
            else if (buildingName.Equals(Building.Farm.ToString()))
            {
                Debug.Log("Building farm");
                Build(Building.Farm);
            }

            // if no building name is found it will simply play the progress bar only
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

                    foreach (var b in visibilityMap.Keys)
                    {
                        // Getting the visibility of the cloud
                        if (b.name.Equals("Cloud" + building.name.ToString()
                                              .Substring(building.name.ToString().Length - 1)))
                        {
                            b.SetActive(true);

                            // Setting cloud for it to get disabled
                            buildingCloud = b;
                        }
                    }
                }

            }
        }

        private void StopBuildingCloud()
        {
            instantiationTimer -= Time.deltaTime;
            if (instantiationTimer <= 0 && buildingCloud != null)
            {
                buildingCloud.SetActive(false);
                instantiationTimer = 2f;
            }

        }
    }
}
