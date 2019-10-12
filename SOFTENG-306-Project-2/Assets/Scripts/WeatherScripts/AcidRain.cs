using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcidRain : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem rain;

    private bool status = false;

    [SerializeField]
    private Button button;

    void Start()
    {
        button.onClick.AddListener(OnClick); 
    }

    /** Right now rain is triggered on button click by the test button, will need to implement a weather controller that fires weather event
     ** as a function of the environmental health 
     */    
    void OnClick()
    {
        if (status){
            status = !status;
            rain.Play();
            Debug.Log(rain.isPlaying);

        } else {
            status = !status;
            rain.Stop();
            Debug.Log(rain.isPlaying);
        }
    }
}
