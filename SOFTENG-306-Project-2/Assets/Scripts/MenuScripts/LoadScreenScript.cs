using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SunnyTown
{
    /// <summary>
    /// The LoadScreenScript is responsible for asynchronously loading a scene while showing the progress on a
    /// slider.
    /// </summary>
    public class LoadScreenScript : MonoBehaviour
    {

        public GameObject LoadingScreenObject;
        public Slider Slider;

        private AsyncOperation async;

        /// <summary>
        /// LoadScreen() takes in a build index to load. It starts the LoadScreen() Coroutine.
        /// </summary>
        /// <param name="level">Build index of scene to load</param>
        public void LoadScreen(int level)
        {
            StartCoroutine(LoadingScreen(level));
        }

        /// <summary>
        /// Asynchronously loads a scene identified by the build index. The progress of this scene transition is then
        /// mapped onto the slider.
        /// </summary>
        /// <param name="level">Build index of scene to load</param>
        /// <returns></returns>
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