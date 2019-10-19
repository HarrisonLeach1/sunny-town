using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBuildingSpawn : MonoBehaviour
{
    public GameObject building;
    float TmStart;
    float TmLen=3f;
 
    // Use this for initialization
    void Start () {
        TmStart=Time.time;
        building.SetActive(false);
    }
     
    // Update is called once per frame
    void Update()
    {
        if (Time.time > TmStart + TmLen)
        {

            building.SetActive(true);
        }

    }
}
