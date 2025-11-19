using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.TransitionSystem
{
    public class LoadingIconAnimator : AdvancedBehaviour
    {
        //public enum AnimationType { Animator, List }
        [SerializeField] private LoadingScreen loadingScreen = default;
        [Space]
        //[SerializeField] private AnimationType animationType = default;
        //[Space]
        //[DrawIf(nameof(animationType), AnimationType.Animator)][SerializeField] private Animator animator = default;
        //[DrawIf(nameof(animationType), AnimationType.Animator)][SerializeField] private string animationSpeedParameterName = "speedMultiplier";

        [SerializeField] private Image img;
        [SerializeField] private float frameWaitTime = 0.08f;
        [SerializeField] private List<Sprite> sprites;


        protected override void Reset()
        {
            base.Reset();
            loadingScreen = FindObjectOfType<LoadingScreen>();
        }

        private void OnEnable()
        {
            //if (animationType == AnimationType.Animator)
            //{
            //    StartCoroutine(PlayIconSlowdownAnimator());
            //}
            //else StartCoroutine(PlayIconSlowdownList());
            StartCoroutine(PlayIconSlowdownList());
        }


        private IEnumerator PlayIconSlowdownList()
        {
            int index = 0;
            bool lastSlowState = false;
            float multiplier = 1f;
            float timer = 0f;
            while (true)
            {
                if (loadingScreen.IsSlowingDown)
                {
                    if (lastSlowState == false)
                    {
                        multiplier = Random.Range(1.2f, 3f);
                        lastSlowState = true;
                    }
                    timer += Time.deltaTime;
                }
                else if (!loadingScreen.IsSlowingDown)
                {
                    if (lastSlowState == true)
                    {
                        lastSlowState = false;
                        multiplier = 1 / multiplier;
                    }
                    if (timer <= 0f)
                    {
                        multiplier = 1f;
                    }
                    else timer -= Time.deltaTime;
                }
                yield return new WaitForSeconds(frameWaitTime * multiplier);

                index = index.WrapIndex(1, sprites.Count);
                img.sprite = sprites[index];
            }
        }

        //private IEnumerator PlayIconSlowdownAnimator()
        //{
        //    animator.SetFloat(animationSpeedParameterName, 1f);

        //    while (true)
        //    {
        //        while (!loadingScreen.IsSlowingDown)
        //        {
        //            yield return null;
        //        }

        //        float multiplier = Random.Range(0.2f, 0.6f);

        //        animator.SetFloat(animationSpeedParameterName, multiplier);

        //        while (loadingScreen.IsSlowingDown)
        //        {
        //            yield return null;
        //        }

        //        animator.SetFloat(animationSpeedParameterName, (2 - multiplier) * 2f);

        //        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));

        //        animator.SetFloat(animationSpeedParameterName, 1);
        //    }
        //}
    }
}