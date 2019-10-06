using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using DefaultNamespace;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    private GameObject[] buildings; 
    private Dictionary<GameObject,bool> visibilityMap = new Dictionary<GameObject,bool>();
    private static Timer aTimer = new Timer();
    private GameObject buildingCloud;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var building in buildings)
        {
            visibilityMap.Add(building, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    // Method called to set building to appear
    public void build(Building buildingName)
    {
        foreach (var building in visibilityMap.Keys)
        {    
            // Setting visibility for the building to true
            if (building.name.Contains(buildingName.ToString()))
            {
                building.GetComponent<SpriteRenderer>().enabled = true;
            }
            // Getting the visibility of the cloud
            if (building.name.Equals("Cloud" + buildingName.ToString().Substring(buildingName.ToString().Length - 1)))
            {
                building.GetComponent<SpriteRenderer>().enabled = true;
                
                // Setting timer to disable the cloud
                aTimer.Interval = 3000;
                aTimer.Elapsed += delegate { StopBuildingCloud(building); };
            }
        }
    }
    
    private void StopBuildingCloud(GameObject building)
    {
        building.GetComponent<SpriteRenderer>().enabled =  false;
    }
}
