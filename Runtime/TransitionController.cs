using System.Collections;
using HexTecGames.Basics;
using HexTecGames.TweenLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HexTecGames.TransitionSystem
{
    public class TransitionController : AdvancedBehaviour
    {
        [SerializeField] private TransitionAnimator transitionAnimator = default;

        public enum SceneSelectType { Name, Index, Next, Previous }

        [Space]
        [SerializeField] private SceneSelectType sceneSelectType = SceneSelectType.Name;
        [SerializeField, DrawIf(nameof(sceneSelectType), SceneSelectType.Name)] private string sceneName = default;
        [SerializeField, DrawIf(nameof(sceneSelectType), SceneSelectType.Index)] private int sceneIndex = default;

        [SerializeField] private LoadingScreen loadingScreen = default;

        private bool isActivated;

        public void Activate()
        {
            if (isActivated)
            {
                return;
            }
            isActivated = true;
            StartCoroutine(PlayTransition());
        }

        private IEnumerator PlayTransition()
        {
            if (transitionAnimator != null)
            {
                yield return transitionAnimator.Play(true);
            }
            if (sceneSelectType == SceneSelectType.Name)
            {
                if (loadingScreen == null)
                {
                    SceneManager.LoadScene(sceneName);
                }
                else loadingScreen.LoadSceneAsync(sceneName);
            }
            else
            {
                if (loadingScreen == null)
                {
                    SceneManager.LoadScene(GetSceneIndex());
                }
                else loadingScreen.LoadSceneAsync(GetSceneIndex());
            }
        }

        private int GetSceneIndex()
        {
            switch (sceneSelectType)
            {
                case SceneSelectType.Index:
                    return sceneIndex;
                case SceneSelectType.Next:
                    return GetNextScene();
                case SceneSelectType.Previous:
                    return GetPreviousScene();
                default:
                    return 0;
            }
        }
        private int GetNextScene()
        {
            int index = SceneManager.GetActiveScene().buildIndex + 1;
            if (index >= SceneManager.sceneCountInBuildSettings)
            {
                index = 0;
            }
            return index;
        }
        private int GetPreviousScene()
        {
            int index = SceneManager.GetActiveScene().buildIndex - 1;
            if (index > 0)
            {
                index = SceneManager.sceneCountInBuildSettings - 1;
            }
            return index;
        }
    }
}