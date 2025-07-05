using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.TransitionSystem
{
    public class LoadingBarAnimator : MonoBehaviour
    {
        [SerializeField] private LoadingScreen loadingScreen = default;
        [SerializeField] private Slider progressSlider = default;

        private void Reset()
        {
            progressSlider = GetComponent<Slider>();
            loadingScreen = FindObjectOfType<LoadingScreen>();
        }

        private void OnEnable()
        {
            StartCoroutine(UpdateProgressBar());
        }

        private IEnumerator UpdateProgressBar()
        {
            while (true)
            {
                progressSlider.value = loadingScreen.Progress + 0.1f;

                while (loadingScreen.IsSlowingDown)
                {
                    yield return null;
                }
                if (Random.Range(0f, 1f) > 0.1f)
                {
                    yield return new WaitForSeconds(Random.Range(0.04f, 0.08f));
                }
                else yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));
            }
        }
    }
}