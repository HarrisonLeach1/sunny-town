using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupButtonScript : MonoBehaviour
{
    //have reference to popup button on start
    public GameObject popup;
    private Animator animator;

    private Button button; 

    // Start is called before the first frame update
    void OnEnable()
    {   popup = GameObject.Find("Popup");
        animator = popup.GetComponent<Animator>();
        AnimatorClipInfo[] clipInfo =  animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        button = animator.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick()
    {
        Debug.Log("something");
    }
}
