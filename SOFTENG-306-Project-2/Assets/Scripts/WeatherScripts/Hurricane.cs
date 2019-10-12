using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Hurricane : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    private Animator animator;

    [SerializeField]
    private Button button;

    private bool showing;

    [SerializeField]
    private ParticleSystem storm; 
    void Start()
    {   
        storm.Stop();
        obj = GameObject.Find("storm");
        animator = obj.GetComponent<Animator>();
        showing = false;
        button.onClick.AddListener(TriggerHurricane);
        animator.SetBool("triggerAnim",false);
    }

    void TriggerHurricane()
    {
        if (!showing){
            Debug.Log("in her e");
            storm.Play();
            animator.SetBool("triggerAnim",true);
            showing = true;
        }
    }

    void onEndOfAnim()
    {
        storm.Stop();
        animator.SetBool("triggerAnim", false);
        Debug.Log("stopped");
        showing = false;
    }
}
