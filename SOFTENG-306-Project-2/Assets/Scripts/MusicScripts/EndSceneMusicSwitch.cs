using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneMusicSwitch : MonoBehaviour
{
    
    public AudioSource _AudioSource;

    public AudioClip winClip;
    public AudioClip loseClip;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayWinClip()
    {
        _AudioSource.clip = winClip;
        _AudioSource.Play();
    }

    public void PlayLoseClip()
    {
        _AudioSource.clip = loseClip;
        _AudioSource.Play();
    }
}
