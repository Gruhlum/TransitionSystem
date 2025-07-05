using UnityEngine;

namespace HexTecGames.TransitionSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/TransitionSystem/LoadingScreenTip")]
    public class LoadingScreenTip : ScriptableObject
    {
        public Sprite Sprite
        {
            get
            {
                return sprite;
            }
            private set
            {
                sprite = value;
            }
        }
        [SerializeField] private Sprite sprite;

        public string Description
        {
            get
            {
                return description;
            }
            private set
            {
                description = value;
            }
        }
        [SerializeField, TextArea] private string description;
    }
}