using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HexTecGames.TransitionSystem
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private float minDuration = default;
        [SerializeField] private float startDelay = 0.5f;
        [SerializeField] private float closeDelay = 0.1f;

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


        public void LoadSceneAsync(string name)
        {
            var result = SceneManager.LoadSceneAsync(name);
            if (result != null)
            {
                LoadSceneAsync(result);
            }
        }
        public void LoadSceneAsync(int index)
        {
            LoadSceneAsync(SceneManager.LoadSceneAsync(index));
        }
        private void LoadSceneAsync(AsyncOperation progress)
        {
            loadingProgress = progress;
            gameObject.SetActive(true);
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
                IsSlowingDown = true;
                yield return new WaitForSeconds(Random.Range(0.5f, 2f));
                IsSlowingDown = false;
                yield return new WaitForSeconds(Random.Range(0.5f, 3f));
            }

        }
        private IEnumerator CheckForProgress()
        {
            yield return new WaitForSeconds(startDelay);
            while (Progress < 0.9f)
            {
                yield return null;

                timer += Time.deltaTime;
                Progress = CalculateProgress();
            }
            yield return new WaitForSeconds(closeDelay);
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