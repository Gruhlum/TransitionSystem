using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.TransitionSystem
{
    public class LoadingScreenTipDisplay : MonoBehaviour
    {
        [SerializeField] private List<LoadingScreenTip> loadingScreenTips = default;
        [SerializeField] private TMP_Text descriptionText = default;
        [SerializeField] private Image descriptionImg = default;
        [SerializeField, Tooltip("How long should this be displayed? 0 for infinite")] private float tipDuration = 0;

        private LoadingScreenTip currentPage;
        private float tipDurationTimer;

        private void OnEnable()
        {
            StartCoroutine(AnimateLoadingScreenTip());
        }

        private LoadingScreenTip GetNextLoadingScreenTip()
        {
            if (currentPage == null)
            {
                return loadingScreenTips.Random();
            }
            else return loadingScreenTips.Next(currentPage);
        }
        private IEnumerator AnimateLoadingScreenTip()
        {
            DisplayLoadingScreenTip(GetNextLoadingScreenTip());

            if (tipDuration <= 0)
            {
                yield break;
            }
            while (true)
            {
                while (tipDurationTimer < tipDuration)
                {
                    yield return null;
                    tipDurationTimer += Time.deltaTime;
                }
                tipDurationTimer = 0;
                DisplayLoadingScreenTip(GetNextLoadingScreenTip());
            }
        }
        private void DisplayLoadingScreenTip(LoadingScreenTip tip)
        {
            descriptionText.text = tip.Description;
            if (descriptionImg != null && tip.Sprite != null)
            {
                descriptionImg.sprite = tip.Sprite;
            }
            currentPage = tip;
        }
    }
}