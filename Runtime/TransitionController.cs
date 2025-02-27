using HexTecGames.Basics;
using HexTecGames.TweenLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HexTecGames.TransitionSystem
{
    public class TransitionController : MonoBehaviour
    {
        //[SerializeField] private List<TweenPlayerBase> tweenPlayers = default;
        [SerializeField] private TransitionAnimator transitionAnimator = default;

        public enum SceneSelectType { Name, Index, Next, Previous }

        [SerializeField] private SceneSelectType sceneSelectType = default;
        [SerializeField, DrawIf(nameof(sceneSelectType), SceneSelectType.Name)] private string sceneName = default;
        [SerializeField, DrawIf(nameof(sceneSelectType), SceneSelectType.Index)] private int sceneIndex = default;

        [SerializeField] private LoadingScreen loadingScreen = default;

        [SerializeField] private float waitTime = default;

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

            transitionAnimator.Play();

            yield return new WaitForSeconds(waitTime);

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