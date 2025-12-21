using System.Collections;
using HexTecGames.EaseFunctions;
using HexTecGames.TweenLib;
using UnityEngine;

namespace HexTecGames.TransitionSystem
{
    public class TwoHalvesTransition : TransitionAnimator
    {
        public RectTransform leftT;
        public RectTransform rightT;

        [Header("Animation")]
        [SerializeField] private EasingType animationType = default;
        [SerializeField] private FunctionType curve = default;
        [SerializeField] private float speed = 1;
        [SerializeField] private bool playInReverse = default;
        [Space]
        [SerializeField] private bool playOnStart = default;

        private void Start()
        {
            if (playOnStart)
            {
                StartCoroutine(Play(false));
            }
        }

        public override IEnumerator Play(bool reverse = false)
        {
            StartCoroutine(AnimateSide(leftT, 1, reverse));
            StartCoroutine(AnimateSide(rightT, -1, reverse));

            yield return new WaitForSeconds(1f / speed);
        }
        private IEnumerator AnimateSide(RectTransform rectT, float multiplier, bool reverse)
        {
            Vector2 startPos = reverse ? rectT.anchoredPosition + new Vector2(leftT.rect.width * multiplier, 0) : rectT.position;
            Vector2 endPos = reverse ? rectT.position : rectT.anchoredPosition + new Vector2(leftT.rect.width * multiplier, 0);
            float timer = 0f;
            rectT.anchoredPosition = startPos;

            System.Func<float, float> function = EaseFunction.GetFunction(animationType, curve);
            while (timer < 1f)
            {
                timer += Time.deltaTime * speed;
                timer = Mathf.Min(timer, 1f);
                float value;
                if (playInReverse)
                {
                    value = function.Invoke(1 - timer);
                }
                else value = function.Invoke(timer);
                rectT.anchoredPosition = Vector3.LerpUnclamped(startPos, endPos, value);
                yield return null;
            }
        }
    }
}