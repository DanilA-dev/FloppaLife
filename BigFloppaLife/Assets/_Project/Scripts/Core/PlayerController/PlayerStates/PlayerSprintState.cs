using D_Dev.PolymorphicValueSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Core.PlayerController.PlayerStates
{
    public class PlayerSprintState : BasePlayerState
    {
        #region Fields

        [Title("Movement Handler")]
        [SerializeField] private PlayerMovementHandler _movementHandler;
        [Title("Sprint Settings")]
        [SerializeReference] private PolymorphicValue<float> _sprintAcceleration;
        [SerializeReference] private PolymorphicValue<float> _sprintMaxSpeed;
        #endregion
        
        #region Properties

        public override PlayerStateName StateName => PlayerStateName.Sprint;

        #endregion

        #region IState

        public override void OnFixedUpdate()
        {
            _movementHandler.ApplyMovement(_sprintAcceleration.Value, _sprintMaxSpeed.Value);
        }

        #endregion
    }
}
