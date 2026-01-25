using D_Dev.PolymorphicValueSystem;
using D_Dev.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Core.PetStates
{
    [System.Serializable]
    public abstract class BaseAgentState : IState
    {
        #region Fields

        [PropertyOrder(100)]
        [Title("Base Settings")]
        [SerializeReference] protected PolymorphicValue<float> _timeoutExit;
        [PropertyOrder(100)]
        protected AgentStateMachineController _controller;
        
        #endregion
        
        #region Properties
        public float ExitTime { get; }

        public float TimeoutExit => _timeoutExit.Value;

        #endregion

        #region Public

        public void Init(AgentStateMachineController controller) => _controller = controller;

        #endregion
        
        #region IState

        public virtual void OnEnter()
        {
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