using System.Collections;
using UnityEngine;

namespace HexTecGames.TransitionSystem
{
    public abstract class TransitionAnimator : MonoBehaviour
    {
        public abstract IEnumerator Play(bool reverse = false);
    }
}