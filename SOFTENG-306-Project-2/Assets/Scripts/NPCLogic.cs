using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLogic: MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotSpeed = 100f;
    
    
    private bool isWandering = false;
    private bool isRotLeft = false;
    private bool isRotRight = false;
    private bool isWalking = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isWandering==false)
        {
            StartCoroutine(Wander());
        }

        if (isRotRight)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);

        }
        
        if (isRotLeft)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);

        }
        if (isWalking)
        {
            //Debug.Log(transform.position);
            transform.position += transform.forward * Time.deltaTime * moveSpeed;

        }
    }

    IEnumerator Wander()
    {

        int rotTime = Random.Range(1, 3);
        int rotWait = Random.Range(1, 4);
        int rotLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        isWandering = true;
        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotWait);
        if (rotLorR == 1)
        {
            isRotRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotRight = false;
        }
        else
        {
            isRotLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotLeft = false;

        }

        isWandering = false;

    }
}
