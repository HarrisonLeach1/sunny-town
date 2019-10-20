using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBuildingSpawn : MonoBehaviour
{
    public GameObject building;
    float TmStart;

 
    void OnEnable()
    {
        building.SetActive(false);
        StartCoroutine (Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds (2);
        building.SetActive(true);
    }
}
