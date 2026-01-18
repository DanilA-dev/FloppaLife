using _Project.Scripts.Core.PetStates;
using D_Dev.StateMachineBehaviour;
using UnityEngine;
using _Project.Scripts.Core.PetView;
using D_Dev.StateMachine;
using Sirenix.OdinInspector;

namespace _Project.Scripts.Core
{
    public class PetStateMachineController : StateMachineBehaviour<PetStateType>
    {
        #region Fields

        [PropertyOrder(-1)]
        [Title("General Settings")]
        [SerializeField] private PetViewHandler _viewHandler;

        [PropertyOrder(-1)]
        [FoldoutGroup("States")] 
        [SerializeField] private IdlePetState _idleState;
        [PropertyOrder(-1)]
        [FoldoutGroup("States")] 
        [SerializeField] private RoamPetState _roamState;

        #endregion

        #region Properties

        public PetViewHandler ViewHandler => _viewHandler;

        #endregion
        
        #region Overrides

        protected override void InitStates()
        {
            AddState(PetStateType.Idle, _idleState);
            AddState(PetStateType.Roaming, _roamState);
            
            var states = new BasePetState[]
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
           AddTransition(new [] { PetStateType.Idle }, PetStateType.Roaming, new DelayCondition(_idleState.TimeoutExit));
           AddTransition(new[] { PetStateType.Roaming }, PetStateType.Idle, new GroupAnyCondition(
               new IStateCondition[]
               {
                   new DelayCondition(_roamState.TimeoutExit),
                   new FuncCondition(_roamState.IsTargetReached)
               }));
        }

        #endregion
    }
}