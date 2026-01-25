using _Project.Scripts.Core.PetStates;
using D_Dev.StateMachineBehaviour;
using UnityEngine;
using _Project.Scripts.Core.PetView;
using D_Dev.StateMachine;
using Sirenix.OdinInspector;
using UnityEngine.AI;

namespace _Project.Scripts.Core
{
    public class AgentStateMachineController : StateMachineBehaviour<AgentStateType>
    {
        #region Fields

        [PropertyOrder(-1)]
        [Title("General Settings")]
        [SerializeField] private AgentViewHandler _viewHandler;
        [PropertyOrder(-1)]
        [SerializeField] private NavMeshAgent _navMeshAgent;

        [PropertyOrder(-1)]
        [FoldoutGroup("Idle")]
        [SerializeField] private IdleAgentState _idleState;
        [PropertyOrder(-1)]
        [FoldoutGroup("Roam")]
        [SerializeField] private RoamAgentState _roamState;

        #endregion

        #region Properties

        public AgentViewHandler ViewHandler => _viewHandler;

        public NavMeshAgent NavMeshAgent => _navMeshAgent;

        #endregion
        
        #region Overrides

        protected override void InitStates()
        {
            AddState(AgentStateType.Idle, _idleState);
            AddState(AgentStateType.Roaming, _roamState);
            
            var states = new BaseAgentState[]
            {
                _idleState,
                _roamState,
            };
            
            foreach (var state in states)
                state.Init(this);
            
            InitTransitions();
        }
        #endregion
        
        #region Private

        private void InitTransitions()
        {
           AddTransition(new [] { AgentStateType.Idle }, AgentStateType.Roaming, new DelayCondition(_idleState.TimeoutExit));
           AddTransition(new[] { AgentStateType.Roaming }, AgentStateType.Idle, new GroupAnyCondition(
               new IStateCondition[]
               {
                   new DelayCondition(_roamState.TimeoutExit),
                   new FuncCondition(_roamState.IsTargetReached)
               }));
        }

        #endregion
    }
}