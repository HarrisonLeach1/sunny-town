using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The EndSceneMusicSwitch is responsible for playing music in the final scene, depending on the game outcome.
/// </summary>
public class EndSceneMusicSwitch : MonoBehaviour
{
    
    public AudioSource _AudioSource;

    public AudioClip winClip;
    public AudioClip loseClip;

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
