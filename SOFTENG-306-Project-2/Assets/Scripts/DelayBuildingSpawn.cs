using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayBuildingSpawn : MonoBehaviour
{
    public GameObject building;
    public Camera mainCamera = null;
    public Camera localCamera = null;

 
    void OnEnable()
    {
        building.SetActive(false);

        if (localCamera != null)
        {
            mainCamera.enabled = false;
            localCamera.enabled = true;   
        }

        StartCoroutine (Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds (2);
        building.SetActive(true);
        if (localCamera != null)
        {
            yield return new WaitForSeconds (2);
            mainCamera.enabled = true;
            localCamera.enabled = false; 
        }

    }
}
