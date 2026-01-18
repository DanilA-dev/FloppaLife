using D_Dev.AnimatorView.AnimationPlayableHandler;
using D_Dev.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Core.PetStates
{
    [System.Serializable]
    public abstract class BasePetState : IState
    {
        #region Fields

        [PropertyOrder(100)]
        [FoldoutGroup("Base Settings")]
        [SerializeField] protected float _timeoutExit;
        [PropertyOrder(100)]
        [FoldoutGroup("Base Settings")]
        [SerializeField] protected AnimationPlayableClipConfig _stateAnimationConfig;

        protected PetStateMachineController _controller;
        
        #endregion
        
        #region Properties
        public float ExitTime { get; }

        public float TimeoutExit => _timeoutExit;

        #endregion

        #region Public

        public void Init(PetStateMachineController controller) => _controller = controller;

        #endregion
        
        #region IState

        public virtual void OnEnter()
        {
            _controller.ViewHandler.PlayAnimationConfig(_stateAnimationConfig);
        }

        public virtual void OnUpdate()
        {
            
        }

        public virtual void OnFixedUpdate()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        #endregion
    }
}