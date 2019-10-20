using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SunnyTown
{
    public class LoadScreenScript : MonoBehaviour
    {

        public GameObject LoadingScreenObject;
        public Slider Slider;

        private AsyncOperation async;

        public void LoadScreen(int level)
        {
            StartCoroutine(LoadingScreen(level));
        }

        IEnumerator LoadingScreen(int level)
        {
            LoadingScreenObject.SetActive(true);
            async = SceneManager.LoadSceneAsync(level);
            async.allowSceneActivation = false;

            while (!async.isDone)
            {
                Slider.value = async.progress;
                if (async.progress == 0.9f)
                {
                    Slider.value = 1f;
                    async.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}