using D_Dev.AnimatorView;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Core.PlayerView
{
    public class PlayerView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private AnimatorView _animatorView;
        [Title("Animations")]
        [SerializeField] private AnimationClipConfig _idleAnimationConfig;
        [SerializeField] private AnimationClipConfig _walkAnimationConfig;
        [SerializeField] private AnimationClipConfig _runAnimationConfig;

        #endregion

        #region Public

        public void PlayIdleAnimation() => _animatorView.PlayAnimation(_idleAnimationConfig);

        public void PlayWalkAnimation() => _animatorView.PlayAnimation(_walkAnimationConfig);

        public void PlayRunAnimation() => _animatorView.PlayAnimation(_runAnimationConfig);
        

        #endregion
    }
}