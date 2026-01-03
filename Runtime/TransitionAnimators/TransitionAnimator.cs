using System.Collections;
using UnityEngine;

namespace HexTecGames.TransitionSystem
{
    public abstract class TransitionAnimator : MonoBehaviour
    {
        public void ActivateAndPlay(bool reverse = false)
        {
            gameObject.SetActive(true);
            StartCoroutine(Play(reverse));
        }

        public abstract IEnumerator Play(bool reverse = false);
    }
}