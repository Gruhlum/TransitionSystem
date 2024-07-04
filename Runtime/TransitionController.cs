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
        [SerializeField] private List<TweenPlayerBase> tweenPlayers = default;

        public enum SceneSelectType { Name, Index, Next, Previous }

        [SerializeField] private SceneSelectType sceneSelectType = default;
        [SerializeField, DrawIf(nameof(sceneSelectType), SceneSelectType.Name)] private string sceneName = default;
        [SerializeField, DrawIf(nameof(sceneSelectType), SceneSelectType.Index)] private int sceneIndex = default;

        [SerializeField] private LoadingScreen loadingScreen = default;

        public void Activate()
        {
            StartCoroutine(PlayTransition());
        }

        private IEnumerator PlayTransition()
        {
            foreach (var tweenPlayer in tweenPlayers)
            {
                tweenPlayer.Play(false);
            }

            while (!CheckIfTransitionIsFinished())
            {
                yield return null;
            }

            loadingScreen.LoadScene(GetSceneIndex());
        }

        private bool CheckIfTransitionIsFinished()
        {
            foreach (var tweenPlayer in tweenPlayers)
            {
                if (tweenPlayer.enabled)
                {
                    return false;
                }
            }
            return true;
        }
        private int GetSceneIndex()
        {
            switch (sceneSelectType)
            {
                case SceneSelectType.Name:
                    return GetSceneIndex(sceneName);
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
        private int GetSceneIndex(string name)
        {
            return SceneManager.GetSceneByName(name).buildIndex;
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