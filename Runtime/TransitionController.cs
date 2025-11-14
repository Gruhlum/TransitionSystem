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

        public void Activate()
        {
            StartCoroutine(PlayTransition());
        }
        //public void Activate(TweenPlayerBase tweenPlayer)
        //{
        //    StartCoroutine(PlayTransition(tweenPlayer));
        //}
        //private IEnumerator PlayTransition(TweenPlayerBase tweenPlayer)
        //{
        //    while (tweenPlayer.IsActive)
        //    {
        //        yield return null;
        //    }
        //    yield return PlayTransition();
        //}
        private IEnumerator PlayTransition()
        {
            //foreach (var tweenPlayer in tweenPlayers)
            //{
            //    tweenPlayer.Play(false);
            //}

            if (transitionAnimator != null)
            {
                yield return transitionAnimator.Play();
            }
            if (sceneSelectType == SceneSelectType.Name)
            {
                loadingScreen.LoadScene(sceneName);
            }
            else loadingScreen.LoadScene(GetSceneIndex());
        }

        //private bool CheckIfTransitionIsFinished()
        //{
        //    foreach (var tweenPlayer in tweenPlayers)
        //    {
        //        if (tweenPlayer.IsActive)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
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