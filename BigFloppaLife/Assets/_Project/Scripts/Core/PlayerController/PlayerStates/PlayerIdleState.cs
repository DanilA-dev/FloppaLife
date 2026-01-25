using UnityEngine;

namespace _Project.Scripts.Core.PlayerController.PlayerStates
{
    public class PlayerIdleState : BasePlayerState
    {
        #region Fields

        [SerializeField] private PlayerMovementHandler _movementHandler;

        #endregion
        
        #region Properties

        public override PlayerStateName StateName => PlayerStateName.Idle;

        #endregion

        #region IState

        public override void OnFixedUpdate()
        {
            _movementHandler.StopMovement();
            _movementHandler.ApplyBraking(15f);
        }

        #endregion
    }
}
