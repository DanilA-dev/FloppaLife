using System.Collections.Generic;
using D_Dev.AnimatorView;
using UnityEngine;

namespace _Project.Scripts.Core.PetView
{
    public class AgentViewHandler : MonoBehaviour
    {
        #region Class

        [System.Serializable]
        public class PetStateAnimationConfig
        {
            public AgentStateType StateType;
            public AnimationClipConfig AnimationConfig;
        }

        #endregion
        
        #region Fields

        [SerializeField] private AnimatorView _animatorView;
        [SerializeField] private List<PetStateAnimationConfig> _stateAnimationConfigs = new();

        #endregion

        #region Public

        public void PlayAnimationConfig(AgentStateType stateType)
        {
            if(_stateAnimationConfigs == null || _stateAnimationConfigs.Count == 0)
                return;
            
            var config = _stateAnimationConfigs.Find(x => x.StateType == stateType);
            if(config == null)
                return;
            
            _animatorView.PlayAnimation(config.AnimationConfig);
        }

        #endregion
    }
}