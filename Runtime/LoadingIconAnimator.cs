using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.TransitionSystem
{
    public class LoadingIconAnimator : MonoBehaviour
    {
        [SerializeField] private LoadingScreen loadingScreen = default;
        [Space]
        [SerializeField] private Animator animator = default;
        [SerializeField] private string animationSpeedParameterName = "speedMultiplier";


        private void Reset()
        {
            animator = GetComponent<Animator>();
            loadingScreen = FindObjectOfType<LoadingScreen>();
        }

        private void OnEnable()
        {
            StartCoroutine(PlayIconSlowdown());
        }

        private IEnumerator PlayIconSlowdown()
        {
            animator.SetFloat(animationSpeedParameterName, 1f);

            while (true)
            {
                while (!loadingScreen.IsSlowingDown)
                {
                    yield return null;
                }

                float multiplier = Random.Range(0.2f, 0.6f);

                animator.SetFloat(animationSpeedParameterName, multiplier);

                while (loadingScreen.IsSlowingDown)
                {
                    yield return null;
                }

                animator.SetFloat(animationSpeedParameterName, (2 - multiplier) * 2f);

                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));

                animator.SetFloat(animationSpeedParameterName, 1);
            }
        }
    }
}