using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using DefaultNamespace;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    public GameObject[] buildings; 
    private Dictionary<GameObject,bool> visibilityMap = new Dictionary<GameObject,bool>();
    private float InstantiationTimer = 3f;
    private GameObject buildingCloud;
    
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
    
    // Method called to set building to appear
    public void build(Building buildingName)
    {
        foreach (var building in visibilityMap.Keys)
        {    
            // Setting visibility for the building to true
            if (building.name.Contains(buildingName.ToString()))
            {
                building.SetActive(true);
                
                foreach (var b in visibilityMap.Keys)
                { // Getting the visibility of the cloud
                    if (b.name.Equals("Cloud" + building.name.ToString().Substring(building.name.ToString().Length - 1)))
                    {
                        print("Cloud" + building.name.Substring(building.name.Length - 1));
                        b.SetActive(true);
                
                        // Setting building timer for it to be disabled
                        buildingCloud = b;
                    }
                }
            }
            
        }
    }
    
    private void StopBuildingCloud()
    {
        InstantiationTimer -= Time.deltaTime;
        if (InstantiationTimer <= 0 && buildingCloud != null)
        {
            buildingCloud.SetActive(false);
            InstantiationTimer = 2f;
        }
            
    }
}
