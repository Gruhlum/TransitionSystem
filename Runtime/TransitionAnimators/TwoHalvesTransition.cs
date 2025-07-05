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

        [Space]
        [SerializeField] private bool playOnStart = default;

        private void Start()
        {
            if (playOnStart)
            {
                Play();
            }
        }

        public override IEnumerator Play()
        {
            StartCoroutine(AnimateSide(leftT, 1));
            StartCoroutine(AnimateSide(rightT, -1));

            yield return new WaitForSeconds(1f / speed);
        }
        private IEnumerator AnimateSide(RectTransform rectT, float multiplier)
        {
            Vector2 startPos = rectT.position;
            //leftT.anchoredPosition += new Vector2(leftT.rect.width * multiplier, 0);
            Vector2 endPos = rectT.anchoredPosition + new Vector2(leftT.rect.width * multiplier, 0);
            float timer = 0;

            System.Func<float, float> function = EaseFunction.GetFunction(animationType, curve);
            while (timer < 1)
            {
                timer += Time.deltaTime * speed;
                timer = Mathf.Min(timer, 1);
                float value = function.Invoke(timer);
                rectT.anchoredPosition = Vector3.Lerp(startPos, endPos, value);
                yield return null;
            }

        }
    }
}