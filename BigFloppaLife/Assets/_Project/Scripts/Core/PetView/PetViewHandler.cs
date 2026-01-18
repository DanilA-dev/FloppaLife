using D_Dev.AnimatorView.AnimationPlayableHandler;
using UnityEngine;

namespace _Project.Scripts.Core.PetView
{
    public class PetViewHandler : MonoBehaviour
    {
        #region Fields

        [SerializeField] protected AnimationClipPlayableMixer _animationPlayable;


        #endregion

        #region Public

        public void PlayAnimationConfig(AnimationPlayableClipConfig animationClipConfig) 
            => _animationPlayable.Play(animationClipConfig);

        #endregion
    }
}