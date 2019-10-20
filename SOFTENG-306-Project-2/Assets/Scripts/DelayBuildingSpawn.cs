using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBuildingSpawn : MonoBehaviour
{
    public GameObject building = null;
    public Camera mainCamera = null;
    public Camera localCamera = null;

 
    void OnEnable()
    {
        if (building != null)
        {
            building.SetActive(false); 
        }
        

        if (localCamera != null)
        {
            mainCamera.enabled = false;
            localCamera.enabled = true;   
        }

        StartCoroutine (Delay());
    }

    IEnumerator Delay()
    {
        if (building != null)
        {
            yield return new WaitForSeconds (2);
            building.SetActive(true);
        }
        
        if (localCamera != null)
        {
            yield return new WaitForSeconds (5);
            mainCamera.enabled = true;
            localCamera.enabled = false; 
        }

    }
}
