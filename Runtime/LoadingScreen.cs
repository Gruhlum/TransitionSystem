using HexTecGames.Basics.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HexTecGames.TransitionSystem
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private GameObject backgroundGO = default;
        [SerializeField] private float minDuration = default;

        public float Progress
        {
            get
            {
                return this.progress;
            }
            private set
            {
                this.progress = value;
            }
        }
        private float progress;
        public bool IsSlowingDown
        {
            get
            {
                return isSlowingDown;
            }
            private set
            {
                isSlowingDown = value;
            }
        }
        private bool isSlowingDown;

        private AsyncOperation loadingProgress;
        private float timer;

        public void LoadScene(string name)
        {
            LoadScene(SceneManager.LoadSceneAsync(name));
        }
        public void LoadScene(int index)
        {
            LoadScene(SceneManager.LoadSceneAsync(index));
        }
        private void LoadScene(AsyncOperation progress)
        {
            loadingProgress = progress;
            backgroundGO.SetActive(true);
            progress.allowSceneActivation = false;
            StartCoroutine(CheckForProgress());
            StartCoroutine(CalculateSlowdown());
        }
        private IEnumerator CalculateSlowdown()
        {
            while (true)
            {
                while (Random.Range(0f, 1f) < 0.9f)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                isSlowingDown = true;
                yield return new WaitForSeconds(Random.Range(0.2f, 1.8f));
                isSlowingDown = false;
                yield return new WaitForSeconds(Random.Range(0.5f, 3f));
            }

        }
        private IEnumerator CheckForProgress()
        {
            while (Progress < 0.9f)
            {
                yield return null;

                timer += Time.deltaTime;
                Progress = CalculateProgress();
            }
            yield return new WaitForSeconds(0.05f);
            loadingProgress.allowSceneActivation = true;
        }
        private float CalculateProgress()
        {
            if (minDuration > 0)
            {
                return Mathf.Min(timer / minDuration, loadingProgress.progress);
            }
            else return loadingProgress.progress;
        }
    }
}